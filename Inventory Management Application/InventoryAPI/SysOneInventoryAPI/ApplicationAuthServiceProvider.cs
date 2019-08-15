using Microsoft.Owin.Security.OAuth;
using System.DirectoryServices.AccountManagement;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Cors;

namespace SysOneInventoryAPI
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ApplicationAuthServiceProvider : OAuthAuthorizationServerProvider
    {
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
            return base.ValidateClientAuthentication(context);
        }

        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var session = HttpContext.Current.Session;
            
            bool result = false;
            PrincipalContext principalContext = new PrincipalContext(ContextType.Domain, "129.103.98.215", context.UserName, context.Password);
            result = principalContext.ValidateCredentials(context.UserName, context.Password);
            UserPrincipal userPrincipal = null;
            if (result == true)
            {
                userPrincipal = UserPrincipal.FindByIdentity(principalContext, context.UserName);
                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                identity.AddClaim(new Claim("Userid", userPrincipal.SamAccountName)); //SamAccountName is users ZID
                identity.AddClaim(new Claim("Username", userPrincipal.DisplayName));
                identity.AddClaim(new Claim("Useremail", userPrincipal.EmailAddress));
                context.Validated(identity);
            }
            else
            {
                context.SetError("invalid_grant", "The username or password is incorrect.");
                return Task.FromResult<object>(null);
            }
            return base.GrantResourceOwnerCredentials(context);
        }
    }
}