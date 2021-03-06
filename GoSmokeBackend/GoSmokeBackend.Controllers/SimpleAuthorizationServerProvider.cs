﻿using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Castle.Windsor;
using GoSmokeBackend.Dao.AuthManagers;
using Microsoft.Owin.Security.OAuth;

namespace GoSmokeBackend.Controllers
{
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        private readonly IWindsorContainer _container;
       
      


        public SimpleAuthorizationServerProvider(IWindsorContainer container)
        {
            _container = container;
        }

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

       
        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            var isFilledClaim = context.Identity.Claims.FirstOrDefault(x => x.Type == "profileFiled");
            if (isFilledClaim != null)
            {
                var isFilled = isFilledClaim.Value;
                context.AdditionalResponseParameters.Add("isFilled",bool.Parse(isFilled));
            }
            return base.TokenEndpoint(context);
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var userManager = _container.Resolve<CustomUserManager>();
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
          
                var user = await userManager.FindByNameAsync(context.UserName);



                if (user == null)
                {
                    context.SetError("invalid_grant", "The user name doesnt Exist");
                    return;
                }
                var passwordCorrect = await userManager.CheckPasswordAsync(user, context.Password);
                if (!passwordCorrect)
                {
                    context.SetError("invalid_grant", "The password is fake");
                    return;
                }



                var identity = new ClaimsIdentity(context.Options.AuthenticationType);

                identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));

                context.Validated(identity);

        }
    }
}