using Services.Application.Extensions;
using Services.Application.Interfaces;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Services.Domain.Entities;
using Services.Domain.Interfaces;
using Microsoft.Extensions.Options;

namespace Services.Application.Services
{
    public class LoginServices : ILoginServices
    {
        private readonly IUser _IUser;
        private readonly ApiSettings _appSettings;

        public LoginServices(IUser iUserLogic, IOptions<ApiSettings> appSettings)
        {
            this._IUser = iUserLogic;

            _appSettings = appSettings.Value;

        }

        public async Task<Result<User>> LoginAsync(User user)
        {
            var result = new Result<User>();

            if (user == null || string.IsNullOrEmpty(user.Email) && string.IsNullOrEmpty(user.Password))
            {
                return result.ResultError("You need to inform the credentials");
            }
            string hashedData = EncodeDataToBase64(user.Password);

            user = await Task.Run(() =>
             {
                 return _IUser.List().FirstOrDefault(x => x.Email.ToLower().Equals(user.Email.ToLower(), StringComparison.OrdinalIgnoreCase) && x.Password.Equals(hashedData));
             });
            if (user == null)
            {
                return result.ResultError("Incorrect credentials!");
            }

            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Email.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(8),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);
            return result.ResultResponse(user);

        }



        public async Task<bool> LoginAsync(string email, string password)
        {
            return await Task.Run(() =>
            {
                string hashedData = EncodeDataToBase64(password);

                return _IUser.List().Any(x => x.Email.ToLower().Equals(email.ToLower(), StringComparison.OrdinalIgnoreCase) && x.Password.Equals(hashedData));
            }
            );
        }
        public async Task<bool> UserIsAdminAsync(string email)
        {
            return await Task.Run(() =>
            {
                return _IUser.List().Any(x => x.Email.ToLower().Equals(email.ToLower(), StringComparison.OrdinalIgnoreCase) && x.Admin);
            }
            );
        }

      
        public static string EncodeDataToBase64(string toBeEncoded)
        {
            byte[] bytes = System.Text.Encoding.Unicode.GetBytes(toBeEncoded);
            byte[] inArray = System.Security.Cryptography.HashAlgorithm.Create("SHA1").ComputeHash(bytes);
            return Convert.ToBase64String(inArray);
        }
    }
}
