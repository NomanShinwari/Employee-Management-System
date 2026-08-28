using System;
using System.Configuration;
using System.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Security.Principal;
using System.Security.Claims;


namespace MyLoginRegistration
{
    public class JwtAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var cookie = httpContext.Request.Cookies["jwtToken"];

            if (cookie == null)
                return false;

            var token = cookie.Value;

            try
            {
                var secretKey = ConfigurationManager.AppSettings["JwtSecret"];
                var key = Encoding.UTF8.GetBytes(secretKey);

                var tokenHandler = new JwtSecurityTokenHandler();

                var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,

                    IssuerSigningKey = new SymmetricSecurityKey(key),

                    ValidateIssuer = false,
                    ValidateAudience = false,

                    ValidateLifetime = true,

                    ClockSkew = TimeSpan.Zero

                }, out SecurityToken validatedToken);
                httpContext.User = principal;
                System.Threading.Thread.CurrentPrincipal = principal;
                return true;
            }

            catch
            {
                return false;
            }
        }
    }
}