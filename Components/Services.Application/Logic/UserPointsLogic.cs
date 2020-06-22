using Services.Application.Interfaces;
using Services.Domain.Entities;
using Services.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Services.Application.Logic
{
    public class UserPointsLogic : IUserPointsLogic
    {
        readonly IUserPoints _IUserPoints;
        string dateFormat = "dd/MM/yyyy";

        public UserPointsLogic(IUserPoints iUserPoints)
        {
            this._IUserPoints = iUserPoints;
        }


        public List<UserPoints> List()
        {
            List<UserPoints> userPoints = _IUserPoints.List();

            return userPoints;
        }
        public List<UserPoints> List(string email)
        {
            IEnumerable<UserPoints> usersList = List();

            if (!string.IsNullOrEmpty(email))
                usersList = usersList.Where(x => x.Email.ToLower().Contains(email.ToLower(), StringComparison.OrdinalIgnoreCase));

            return usersList.ToList();
        }


        /// <summary>
        /// get UserPoints 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public UserPoints GetEntity(Guid id)
        {
            UserPoints userPoints = _IUserPoints.GetEntity(id);

            return userPoints;
        }

        public int Add(UserPoints entity)
        {
            return this._IUserPoints.Add(entity);
        }

        public Result<UserPoints> AddEntity(UserPoints entity)
        {
            var result = new Result<UserPoints>();

            if (this.List().Any(x => x.Email.Equals(entity.Email, StringComparison.OrdinalIgnoreCase)
            ))
            {
                return result.ResultError("This UserPoints has already been registered!");
            }
            try
            {
                entity.Id = Guid.NewGuid();

                var save = this.Add(entity);

                return (save > 0 ? result.ResultResponse(
              this.List()
              .First(x => x.Id == entity.Id))
              : throw new Exception());

            }
            catch (Exception exc)
            {
                return result.ResultError(exc.Message);
            }

        }

        public int Delete(UserPoints entity)
        {
            return this._IUserPoints.Delete(entity);
        }

        public int? Delete(Guid id)
        {
            var entity = this.GetEntity(id);
            if (entity == null) return null;
            return this.Delete(entity);
        }

        public int Update(UserPoints entity)
        {
            return this._IUserPoints.Update(entity);
        }
        /// <summary>
        /// update user
        /// </summary>
        /// <param name="identifier"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Result<UserPoints> Update(Guid identifier, UserPoints entity)
        {
            var result = new Result<UserPoints>();

            var resource = GetEntity(identifier);
            if (resource == null) return result.ResultError("Resource not found!");

            var dateFormat = "{0:dd/MM/yyyy}";
            try
            {

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

                if (this.Update(resource) <= 0) throw new Exception();
                else return result.ResultResponse(resource);

            }
            catch (Exception exc)
            {
                return result.ResultError(exc.Message);
            }

        }

        public Result<UserPoints> AddingPoints(UserPoints entity)
        {
            var result = new Result<UserPoints>();

            var user = List(entity.Email).FirstOrDefault();
            if (user == null) return result.ResultError("Email not found!");

            try
            {
                user.Points = CalculatePoints(user.Points, entity.Points);

                if (this.Update(user) <= 0) throw new Exception();
                else return result.ResultResponse(user);

            }
            catch (Exception exc)
            {
                return result.ResultError(exc.Message);
            }

        }

        private int CalculatePoints(int value1, int value2)
        {

            if (value1 > value2) return (value1 + value2);
            return value2 + value1;

        }

        public static object GetPropertyValue(object src, string propName)
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

