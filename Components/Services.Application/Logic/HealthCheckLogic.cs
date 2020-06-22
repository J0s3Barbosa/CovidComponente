using Services.Application.Interfaces;
using Services.Domain.Entities;
using Services.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Application.Logic
{
    public class HealthCheckLogic : IHealthCheckLogic
    {
        readonly IHealthCheck _IHealthCheck;
        string dateFormat = "dd/MM/yyyy";
        string dateTimeFormat = "MM/dd/yyyy HH:mm:ss";

        public HealthCheckLogic(IHealthCheck iHealthCheck)
        {
            this._IHealthCheck = iHealthCheck;
        }


        public async Task<List<HealthCheck>> ListAsync()
        {
            List<HealthCheck> healthChecks = await _IHealthCheck.ListAsync();

            return healthChecks;
        }

        public async Task<List<HealthCheck>> ListAsync(string appName)
        {
            IEnumerable<HealthCheck> healthChecks = await ListAsync();

            if (!string.IsNullOrEmpty(appName))
                healthChecks = await Task.Run(() => healthChecks.Where(x => x.AppName.Contains(appName, StringComparison.OrdinalIgnoreCase)));
            return healthChecks.ToList();
        }

        /// <summary>
        /// get HealthCheck 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<HealthCheck> GetEntityAsync(Guid id)
        {
            HealthCheck healthCheck = await _IHealthCheck.GetEntityAsync(id);

            return healthCheck;
        }

        public async Task<int> AddAsync(HealthCheck healthCheck)
        {
            return await this._IHealthCheck.AddAsync(healthCheck);
        }

        public async Task<Result<HealthCheck>> AddHealthCheckAsync(HealthCheck addHealthCheck)
        {
            var result = new Result<HealthCheck>();
            try
            {
                var healthCheck = new HealthCheck
                {
                    Id = Guid.NewGuid(),
                    AppName = addHealthCheck.AppName,
                    TimeAccessed = DateTime.Now.ToString(dateTimeFormat),

                };

                var save = await this.AddAsync(healthCheck);
                return (save > 0 ? result.ResultResponse(
                   this.ListAsync().GetAwaiter().GetResult().First(x => x.Id == healthCheck.Id))
                    : throw new Exception());

            }
            catch (Exception exc)
            {
                return result.ResultError(exc.Message);
            }


        }

        public async Task<int> DeleteAsync(HealthCheck healthCheck)
        {
            return await this._IHealthCheck.DeleteAsync(healthCheck);
        }

        public async Task<int?> DeleteAsync(Guid id)
        {
            var healthCheck = await this.GetEntityAsync(id);
            if (healthCheck == null) return null;
            return await this.DeleteAsync(healthCheck);
        }

        public async Task<int> UpdateAsync(HealthCheck healthCheck)
        {
            return await this._IHealthCheck.UpdateAsync(healthCheck);
        }
        /// <summary>
        /// update healthCheck
        /// </summary>
        /// <param name="identifier"></param>
        /// <param name="healthCheck"></param>
        /// <returns></returns>
        public async Task<Result<HealthCheck>> UpdateAsync(Guid identifier, HealthCheck healthCheck)
        {
            var result = new Result<HealthCheck>();

            var resource = await this.GetEntityAsync(identifier);
            if (resource == null) return result.ResultError("Resource not found!");

            //use reflection to check wich properties have changed and set proper value
            foreach (var entityProp in healthCheck.GetType().GetProperties())
            {
                var entityValue = entityProp.GetValue(healthCheck);

                if (entityProp.Name != "Id" && (!string.IsNullOrEmpty(entityValue as string) || entityValue != null))
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
        /// copy healthCheck selected and change it into a new one with new ID, due date to next month
        /// </summary>
        /// <param name="identifier"></param>
        /// <returns></returns>
        public async Task<Result<HealthCheck>> CopyHealthCheckAsync(Guid identifier)
        {
            var result = new Result<HealthCheck>();

            var resource = await this.GetEntityAsync(identifier);
            if (resource == null) return result.ResultError("Resource not found!");


            resource.Id = Guid.NewGuid();
            resource.TimeAccessed = string.Empty;

            var save = await this.AddAsync(resource);
            return (save > 0 ? result.ResultResponse(
                this.ListAsync().GetAwaiter().GetResult()
                .First(x => x.Id == resource.Id))
                : result.ResultError("Could not save this Resource!"));
        }

        public async Task<Result<string>> CalculateTaskTimeAsync(string start, string finish)
        {
            var result = new Result<string>();
            string res = string.Empty;
            string err = string.Empty;
            try
            {
                if (string.IsNullOrEmpty(finish) || string.IsNullOrWhiteSpace(finish))
                {
                    finish = DateTime.Now.ToString(dateTimeFormat);
                }
                await Task.Run(() =>
                {
                    res = getDiff(start, finish);

                });
            }
            catch (Exception exc)
            {
                err = exc.Message;
            }

            return (!string.IsNullOrEmpty(res) ? result.ResultResponse(
             res
               )
               : result.ResultError(err));
        }

        private string getDiff(string start, string finish)
        {
            try
            {
                var res = string.Empty;
                var dateOne = Convert.ToDateTime(start);
                var dateTwo = Convert.ToDateTime(finish);
                var diff = dateTwo.Subtract(dateOne);
                var days = (int)(diff.TotalDays % 365);
                if (days != 0)
                {
                    res = String.Format("{0} years {1} months {2} days {3}:{4}:{5}", (int)diff.TotalDays / 365,
(int)(diff.TotalDays % 365) / 30, (int)(diff.TotalDays % 365) / 30, diff.Hours, diff.Minutes, diff.Seconds);

                }
                else
                {
                    res = String.Format("{0}:{1}:{2}", diff.Hours, diff.Minutes, diff.Seconds);
                }

                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
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

