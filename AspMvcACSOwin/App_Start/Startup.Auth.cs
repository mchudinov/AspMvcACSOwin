using System.Configuration;
using System.Threading.Tasks;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.WsFederation;
using Owin;

namespace AspMvcACSOwin
{
    public partial class Startup
    {
        private static readonly string _realm = ConfigurationManager.AppSettings["ida:Wtrealm"];
        private static readonly string _adfsMetadata = ConfigurationManager.AppSettings["ida:ADFSMetadata"];

        public void ConfigureAuth(IAppBuilder app)
        {
            app.SetDefaultSignInAsAuthenticationType(CookieAuthenticationDefaults.AuthenticationType);
            app.UseCookieAuthentication(new CookieAuthenticationOptions());
            
            var notifications = new WsFederationAuthenticationNotifications();
            notifications.SecurityTokenReceived = (context) =>
            {
                return Task.FromResult(0);
            };

            app.UseWsFederationAuthentication(
                new WsFederationAuthenticationOptions
                {
                    Notifications = notifications,
                    Wtrealm = _realm,
                    MetadataAddress = _adfsMetadata
                });
        }
    }
}