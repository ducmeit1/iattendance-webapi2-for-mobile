using IAttendanceWebAPI.Core.Repositories;
using IAttendanceWebAPI.Persistence;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IAttendanceWebAPI.Providers
{
    public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
    {
        private readonly string _publicClientId;
        private readonly IUnitOfWork _unitOfWork = new UnitOfWork();

        public ApplicationOAuthProvider(string publicClientId)
        {
            _publicClientId = publicClientId ?? throw new ArgumentNullException("publicClientId");
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var userManager = context.OwinContext.GetUserManager<ApplicationUserManager>();

            var user = await userManager.FindAsync(context.UserName, context.Password);

            if (user == null)
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                return;
            }

            var oAuthIdentity = await user.GenerateUserIdentityAsync(userManager,
                OAuthDefaults.AuthenticationType);
            var cookiesIdentity = await user.GenerateUserIdentityAsync(userManager,
                CookieAuthenticationDefaults.AuthenticationType);

            oAuthIdentity.AddClaim(new Claim("Id", user.Id));
            oAuthIdentity.AddClaim(new Claim("Username", user.UserName));
            oAuthIdentity.AddClaim(new Claim("Email", user.Email));
            oAuthIdentity.AddClaim(new Claim("Name", user.Name));
            oAuthIdentity.AddClaim(new Claim("LoggedOn", DateTime.Now.ToString(CultureInfo.InvariantCulture)));
            oAuthIdentity.AddClaim(new Claim("DateOfBirth", user.DateOfBirth.ToString(CultureInfo.InvariantCulture)));
            var userRoles = userManager.GetRoles(user.Id);
            foreach (var roleName in userRoles) oAuthIdentity.AddClaim(new Claim(ClaimTypes.Role, roleName));
            var identity = userRoles.Contains("Student") ? (await _unitOfWork.Students.GetStudent(user.Id)).Id :
                userRoles.Contains("Teacher") ? (await _unitOfWork.Teachers.GetTeacher(user.Id)).Id : null;

            var additionalDatas = new AuthenticationProperties(new Dictionary<string, string>
            {
                {"user_id", user.Id},
                {"username", user.UserName},
                {"identity", identity},
                {"role", JsonConvert.SerializeObject(userRoles)}
            });
            var ticket = new AuthenticationTicket(oAuthIdentity, additionalDatas);
            context.Validated(ticket);
            context.Request.Context.Authentication.SignIn(cookiesIdentity);
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (var property in context.Properties.Dictionary)
                context.AdditionalResponseParameters.Add(property.Key, property.Value);

            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            // Resource owner password credentials does not provide a client ID.
            if (context.ClientId == null) context.Validated();

            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
        {
            if (context.ClientId == _publicClientId)
            {
                var expectedRootUri = new Uri(context.Request.Uri, "/");

                if (expectedRootUri.AbsoluteUri == context.RedirectUri) context.Validated();
            }

            return Task.FromResult<object>(null);
        }
    }
}