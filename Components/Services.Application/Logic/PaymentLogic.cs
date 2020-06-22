using Services.Application.Interfaces;
using Services.Domain.Entities;
using Services.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Application.Logic
{
    public class PaymentLogic : IPaymentLogic
    {
        readonly IPayment _IPayment;
        readonly IUserLogic _IUser;
        string dateFormat = "dd/MM/yyyy";

        public PaymentLogic(IPayment iPayment, IUserLogic iUser)
        {
            this._IPayment = iPayment;
            this._IUser = iUser;
        }


        public async Task<List<Payment>> ListAsync()
        {
            List<Payment> payments = await _IPayment.ListAsync();

            return payments;
        }

        public async Task<List<Payment>> ListAsync(string userEmail, string description, string dueDate, string paymentType, string cost, string month)
        {
            if (string.IsNullOrEmpty(userEmail) || string.IsNullOrWhiteSpace(userEmail))
                throw new ArgumentException("UserEmail cannot be null.");

            IEnumerable<Payment> paymentList = await ListAsync();

            paymentList = await CheckAndSortPayments(userEmail, description, dueDate, paymentType, cost, month, paymentList);

            return paymentList.OrderByDescending(x => x.DueDate).ToList();
        }

        private async Task<IEnumerable<Payment>> CheckAndSortPayments(string userEmail, string description, string dueDate, string paymentType, string cost, string month, IEnumerable<Payment> paymentList)
        {
            var userInfo = await _IUser.GetUser(userEmail);

            if (!userInfo.Admin)
            {
                paymentList = await Task.Run(() => paymentList.Where(x => x.UserEmail.Equals(userEmail)));
            }

            if (!string.IsNullOrEmpty(description))
                paymentList = await Task.Run(() => paymentList.Where(x => x.Description.Contains(description, StringComparison.OrdinalIgnoreCase)));

            if (!string.IsNullOrEmpty(paymentType))
                paymentList = await Task.Run(() => paymentList.Where(x => x.PaymentType.Contains(paymentType, StringComparison.OrdinalIgnoreCase)));

            if (!string.IsNullOrEmpty(cost))
                paymentList = await Task.Run(() => paymentList.Where(x => x.Cost.Contains(cost, StringComparison.OrdinalIgnoreCase)));

            if (!string.IsNullOrEmpty(dueDate))
                paymentList = await Task.Run(() => paymentList.Where(x => x.DueDate == dueDate));
            if (!string.IsNullOrEmpty(month))
            {
                paymentList = await Task.Run(() => paymentList
       .Where(x => Convert.ToDateTime(x.DueDate).Month.ToString().Contains(month)).ToList());
            }

            return paymentList;
        }

        private async Task<IEnumerable<Payment>> CheckUserIsAdmin(string userEmail, IEnumerable<Payment> paymentList)
        {
            var userInfo = await _IUser.GetUser(userEmail);

            if (!userInfo.Admin)
            {
                paymentList = await Task.Run(() => paymentList.Where(x => x.UserEmail.Equals(userEmail)));
            }
            return paymentList;
        }


        /// <summary>
        /// get Payment 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Payment> GetEntityAsync(Guid id)
        {
            Payment payment = await _IPayment.GetEntityAsync(id);

            return payment;
        }

        public async Task<int> AddAsync(Payment entity)
        {
            return await this._IPayment.AddAsync(entity);
        }

        public async Task<Result<Payment>> AddEntityAsync(Payment entity)
        {
            var result = new Result<Payment>();
            var list = await this.ListAsync();
            if (list.Any(x => x.Description.Equals(entity.Description, StringComparison.OrdinalIgnoreCase)
            && (x.PaidDate == null
            || x.BarCode == entity.BarCode)
            ))
            {
                return result.ResultError("This Bill has already been registered!");
            }

            entity.Id = Guid.NewGuid();

            var save = await this.AddAsync(entity);
            return (save > 0 ? result.ResultResponse(
               this.ListAsync().GetAwaiter().GetResult().First(x => x.Id == entity.Id))
                : result.ResultError("Could not save this bill!"));
        }

        public async Task<int> DeleteAsync(Payment entity)
        {
            return await this._IPayment.DeleteAsync(entity);
        }

        public async Task<int?> DeleteAsync(Guid id)
        {
            var entity = await this.GetEntityAsync(id);
            if (entity == null) return null;
            return await this.DeleteAsync(entity);
        }

        public async Task<int> UpdateAsync(Payment entity)
        {
            return await this._IPayment.UpdateAsync(entity);
        }
        /// <summary>
        /// update payment
        /// </summary>
        /// <param name="identifier"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<Result<Payment>> UpdateAsync(Guid identifier, Payment entity)
        {
            var result = new Result<Payment>();

            var resource = await this.GetEntityAsync(identifier);
            if (resource == null) return result.ResultError("Resource not found!");

            var dateFormat = "{0:dd/MM/yyyy}";

            //use reflection to check wich properties have changed and set proper value
            foreach (var entityProp in entity.GetType().GetProperties())
            {
                var entityValue = entityProp.GetValue(entity);

                if (entityProp.Name != "Id" && !string.IsNullOrEmpty(entityValue as string))
                {
                    foreach (var resourceProp in resource.GetType().GetProperties().Where(x => x.Name == entityProp.Name))
                    {
                        var resourceValue = resourceProp.GetValue(resource);

                        if (string.IsNullOrEmpty(resourceValue as string) && entityValue != null || !resourceValue.ToString().ToLower().Equals(entityValue.ToString().ToLower()))
                        {
                            resourceProp.SetValue(resource, entityValue, null);
                        }
                    }
                }
            }


            if (await this.UpdateAsync(resource) <= 0) return result.ResultError("The resource was not updated!");
            else return result.ResultResponse(resource);
        }
        /// <summary>
        /// copy payment selected and change it into a new one with new ID, due date to next month
        /// </summary>
        /// <param name="identifier"></param>
        /// <returns></returns>
        public async Task<Result<Payment>> NewPaymentAsync(Guid identifier)
        {
            var result = new Result<Payment>();

            var resource = await this.GetEntityAsync(identifier);
            if (resource == null) return result.ResultError("Resource not found!");


            resource.Id = Guid.NewGuid();
            resource.PaidDate = string.Empty;
            DateTime dt =
    DateTime.Parse(resource.DueDate, CultureInfo.GetCultureInfo("pt-BR"));

            resource.DueDate = dt.AddMonths(1).ToString(dateFormat);

            var save = await this.AddAsync(resource);
            return (save > 0 ? result.ResultResponse(
                this.ListAsync().GetAwaiter().GetResult()
                .First(x => x.Id == resource.Id))
                : result.ResultError("Could not save this bill!"));
        }

        public async Task<Result<string>> CalculatePaymentsAsync(List<Payment> listPayments)
        {
            var result = new Result<string>();
            decimal totalPaid = 0;
            string total = string.Empty;
            string err = string.Empty;
            try
            {
                await Task.Run(() =>
                {
                    foreach (var item in listPayments)
                    {
                        var val = item.Cost.Replace(".", "").Replace(",", ".");

                        decimal value = decimal.Parse(val);

                        totalPaid = totalPaid + value;
                    }
                });


                total = String.Format("{0:C}", totalPaid);
            }
            catch (Exception exc)
            {
                err = exc.Message;
            }

            return (!string.IsNullOrEmpty(total) ? result.ResultResponse(
             total
               )
               : result.ResultError(err));
        }

        private static object GetPropertyValue(object src, string propName)
        {
            if (src == null) throw new ArgumentException("Value cannot be null.", "src");
            if (propName == null) throw new ArgumentException("Value cannot be null.", "propName");

            if (propName.Contains("."))//complex type nested
            {
                var temp = propName.Split(new char[] { '.' }, 2);
                return GetPropertyValue(GetPropertyValue(src, temp[0]), temp[1]);
            }
            else
            {
                var prop = src.GetType().GetProperty(propName);
                return prop != null ? prop.GetValue(src, null) : null;
            }
        }




    }

}

