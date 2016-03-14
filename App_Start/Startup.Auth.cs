using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Owin;
using tech4mUI.Models;
using System.Configuration;
using Microsoft.Owin.Security.Facebook;
using System.Threading.Tasks;
using System.Security.Claims;
using Owin.Security.Providers.LinkedIn;

namespace tech4mUI
{
    public partial class Startup
    {
        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Configure the db context, user manager and signin manager to use a single instance per request
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);

            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            // Configure the sign in cookie
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    // Enables the application to validate the security stamp when the user logs in.
                    // This is a security feature which is used when you change a password or add an external login to your account.  
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
                }
            });
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Enables the application to temporarily store user information when they are verifying the second factor in the two-factor authentication process.
            app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));

            // Enables the application to remember the second login verification factor such as phone or email.
            // Once you check this option, your second step of verification during the login process will be remembered on the device where you logged in from.
            // This is similar to the RememberMe option when you log in.
            app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);

            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: ""); 
            //app.UseTwitterAuthentication(
            //   consumerKey: "",
            //   consumerSecret: ""); 
            //var facbookOptions=new FacebookAuthenticationOptions
            //{}

            //app.UseFacebookAuthentication(
            //              appId: ConfigurationManager.AppSettings["Facebook_AppId"].ToString(),
            //              appSecret:  ConfigurationManager.AppSettings["Facebook_AppSecret"].ToString();

            #region Linkedin Authentication
            var linkedInOptions = new LinkedInAuthenticationOptions
            {
                ClientId = ConfigurationManager.AppSettings["LinkedIn_ClientId"].ToString(),
                ClientSecret = ConfigurationManager.AppSettings["LinkedIn_ClientSecret"].ToString(),
                //CallbackPath = new PathString("/oauth-redirect/linkedin"),
                Provider = new LinkedInAuthenticationProvider
                {
                    OnAuthenticated = async context =>
                    {
                        // Retrieve the OAuth access token to store for subsequent API calls
                        string accessToken = context.AccessToken;

                        // Retrieve the username
                        string linkedInUserName = context.UserName;

                        // Retrieve the user's email address
                        string linkedInEmailAddress = context.Email;

                        // You can even retrieve the full JSON-serialized user
                        var serializedUser = context.User;
                    }
                }
            };
            app.UseLinkedInAuthentication(linkedInOptions);
            #endregion Linkedin Authentication

            #region Facebok Authentication
            var facbookOptions = new FacebookAuthenticationOptions
            {
                AppId = ConfigurationManager.AppSettings["Facebook_AppId"].ToString(),
                AppSecret = ConfigurationManager.AppSettings["Facebook_AppSecret"].ToString(),
                Provider = new FacebookAuthenticationProvider()
                {
                    OnAuthenticated = (context) =>
                    {
                        context.Identity.AddClaim(new System.Security.Claims.Claim("FacebookAccessToken", context.AccessToken));
                        return Task.FromResult(0);
                    }
                },
                SignInAsAuthenticationType = DefaultAuthenticationTypes.ExternalCookie,
                SendAppSecretProof = true
            };
            facbookOptions.Scope.Add("email user_friends user_about_me user_birthday user_location");

            app.UseFacebookAuthentication(facbookOptions);
            #endregion Facebok Authentication
            #region Google Authentication
            var googleOptions = new GoogleOAuth2AuthenticationOptions()
             {
                 ClientId = "48018443500-6778ov8b3kvg5prscq85uhempiobbpcv.apps.googleusercontent.com",
                 ClientSecret = "gt8kk4nOeCKxpK48K3hLQP3u",
                 Provider = new GoogleOAuth2AuthenticationProvider()
                 {
                     OnAuthenticated = (context) =>
                     {
                         context.Identity.AddClaim(new Claim("urn:google:name", context.Identity.FindFirstValue(ClaimTypes.Name)));
                         context.Identity.AddClaim(new Claim("urn:google:email", context.Identity.FindFirstValue(ClaimTypes.Email)));
                         //This following line is need to retrieve the profile image
                         context.Identity.AddClaim(new System.Security.Claims.Claim("urn:google:accesstoken", context.AccessToken, ClaimValueTypes.String, "Google"));

                         return Task.FromResult(0);
                     }
                 }
             };

            app.UseGoogleAuthentication(googleOptions);
            #endregion Google Authentication
        }
    }
}