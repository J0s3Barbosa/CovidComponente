using Services.Application.Interfaces;
using Services.Domain.Entities;
using Services.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Services.Application.Logic
{
    public class UserLogic : IUserLogic
    {
        readonly IUser _IUser;
        readonly ILoginServices _ILoginServices;

        public UserLogic(IUser iUser, ILoginServices iLoginServices)
        {
            this._IUser = iUser;
            this._ILoginServices = iLoginServices;
        }

        public List<User> List()
        {
            List<User> users = _IUser.List();

            return users;
        }

        public async Task<List<User>> ListUser(string email)
        {
            ValidateEmail(email);
            var isAdmin = await _ILoginServices.UserIsAdminAsync(email);
            if (!isAdmin) return null;

            return List();

        }



        public async Task<User> GetUser(string email)
        {
            ValidateEmail(email);

            IEnumerable<User> usersList = await Task.Run(() => List());

            return usersList.FirstOrDefault(x => x.Email.ToLower().Equals(email.ToLower()));
        }

        private static void ValidateEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
                throw new ArgumentException("User email must be informed!");
        }

      


        /// <summary>
        /// get User 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public User GetEntity(Guid id)
        {
            User aiplane = _IUser.GetEntity(id);

            return aiplane;
        }

        public int Add(User entity)
        {
            return this._IUser.Add(entity);
        }

        public Result<User> AddEntity(User entity)
        {
            var result = new Result<User>();

            if (this.List().Any(x => x.Email.Equals(entity.Email, StringComparison.OrdinalIgnoreCase)
            ))
            {
                return result.ResultError("This User has already been registered!");
            }
            try
            {
                entity.Id = Guid.NewGuid();

                string hashedData = EncodeDataToBase64(entity.Password);

                entity.Password = hashedData;

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

       
        public static string EncodeDataToBase64(string toBeEncoded)
        {
            byte[] bytes = System.Text.Encoding.Unicode.GetBytes(toBeEncoded);
            byte[] inArray = System.Security.Cryptography.HashAlgorithm.Create("SHA1").ComputeHash(bytes);
            return Convert.ToBase64String(inArray);
        }

        public int Delete(User entity)
        {
            return this._IUser.Delete(entity);
        }

        public int? Delete(Guid id)
        {
            var entity = this.GetEntity(id);
            if (entity == null) return null;
            return this.Delete(entity);
        }

        public int Update(User entity)
        {
            return this._IUser.Update(entity);
        }
        /// <summary>
        /// update user
        /// </summary>
        /// <param name="identifier"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Result<User> Update(Guid identifier, User entity)
        {
            var result = new Result<User>();

            var resource = GetEntity(identifier);
            if (resource == null) return result.ResultError("Resource not found!");
            try
            {

                //use reflection to check wich properties have changed and set proper value
                foreach (var entityProp in entity.GetType().GetProperties())
                {
                    var entityValue = entityProp.GetValue(entity);

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

                if (this.Update(resource) <= 0) throw new Exception();
                else return result.ResultResponse(resource);

            }
            catch (Exception exc)
            {
                return result.ResultError(exc.Message);
            }

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

