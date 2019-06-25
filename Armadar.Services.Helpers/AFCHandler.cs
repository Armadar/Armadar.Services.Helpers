using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Armadar.Services.Helpers
{
    public class AFCHandler
    {
        public static void setCustomError(AuthenticationFailedContext context)
        {
            context.Response.StatusCode = 403;
            context.Response.ContentType = "application/json";

            var err = "";

            if (context.Exception.GetType() == typeof(SecurityTokenValidationException))
            {
                err = "invalid token";
            }
            else if (context.Exception.GetType() == typeof(SecurityTokenInvalidIssuerException))
            {
                err = "invalid issuer";
            }
            else if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
            {
                err = "token expired";
            }

            var resp = new
            {
                error = err,
                status = context.Response.StatusCode
            };

            context.Response.WriteAsync(JsonConvert.SerializeObject(resp, Formatting.Indented));
        }
    }
}