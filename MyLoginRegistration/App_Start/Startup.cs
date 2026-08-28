using System;
using System.Configuration;
using System.Security.Claims;
using System.Text;
using System.Web.Helpers;

using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Microsoft.Owin.Security.Jwt;

using Owin;

using Microsoft.AspNet.Identity;

[assembly: OwinStartup(typeof(MyLoginRegistration.Startup))]

namespace MyLoginRegistration
{
    public class Startup
    {

        public void Configuration(IAppBuilder app)
        {


            // Fix AntiForgery validation with JWT claims
            AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.NameIdentifier;

            app.SetDefaultSignInAsAuthenticationType(CookieAuthenticationDefaults.AuthenticationType);

            // Cookie Authentication
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = CookieAuthenticationDefaults.AuthenticationType
            });

            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Google OAuth Authentication
            app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions
            {
                ClientId = ConfigurationManager.AppSettings["GoogleClientId"],

                ClientSecret = ConfigurationManager.AppSettings["GoogleClientSecret"],

                CallbackPath = new PathString("/signin-google")
            });

            // JWT Authentication

            var secret = ConfigurationManager.AppSettings["JwtSecret"];


            app.UseJwtBearerAuthentication(new JwtBearerAuthenticationOptions
            {
                AuthenticationMode = AuthenticationMode.Active,

                TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,

                    IssuerSigningKey =
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(secret)
                    ),


                    ValidateIssuer = false,

                    ValidateAudience = false,


                    ValidateLifetime = true,

                    ClockSkew = TimeSpan.Zero
                }
            });
        }
    }
}