using Microsoft.Extensions.DependencyInjection;
using Services.Application.Interfaces;
using Services.Application.Logic;
using System;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Services.Application.Services
{
    public class BasicAuthenticationAttribute : AuthorizationFilterAttribute
    {
        private readonly ILoginServices _iUserLogic;

        public BasicAuthenticationAttribute()
        {

            var services = new ServiceCollection();
            var provider = services.BuildServiceProvider();
            _iUserLogic = provider.GetService<ILoginServices>();

        }

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (actionContext.Request.Headers.Authorization == null)
            {
                actionContext.Response = actionContext.Request
                    .CreateResponse(HttpStatusCode.Unauthorized);
            }
            else
            {
                string authenticationToken = actionContext.Request.Headers
                                            .Authorization.Parameter;
                string decodedAuthenticationToken = Encoding.UTF8.GetString(
                    Convert.FromBase64String(authenticationToken));
                string[] usernamePasswordArray = decodedAuthenticationToken.Split(':');
                string username = usernamePasswordArray[0];
                string password = usernamePasswordArray[1];

                if (_iUserLogic.LoginAsync(username, password).GetAwaiter().GetResult())
                {
                    Thread.CurrentPrincipal = new GenericPrincipal(
                        new GenericIdentity(username), null);
                }
                else
                {
                    actionContext.Response = actionContext.Request
                        .CreateResponse(HttpStatusCode.Unauthorized);
                }
            }
        }
    }
}
