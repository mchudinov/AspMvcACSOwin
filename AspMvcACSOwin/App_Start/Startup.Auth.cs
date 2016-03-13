using System.Configuration;
using System.Diagnostics;
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

            var notifications = new WsFederationAuthenticationNotifications
            {
                SecurityTokenReceived = (context) =>
                {
                    Debug.WriteLine("SecurityTokenReceived");
                    return Task.CompletedTask;
                },

                AuthenticationFailed = (context) =>
                {
                    Debug.WriteLine("AuthenticationFailed");
                    return Task.CompletedTask;
                },                
                
                MessageReceived = (context) =>
                {
                    Debug.WriteLine("MessageReceived");
                    return Task.CompletedTask;
                },

                RedirectToIdentityProvider = (context) =>
                {
                    Debug.WriteLine("RedirectToIdentityProvider");
                    return Task.CompletedTask;
                },

                SecurityTokenValidated = (context) =>
                {
                    Debug.WriteLine("SecurityTokenValidated");
                    return Task.CompletedTask;
                }
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