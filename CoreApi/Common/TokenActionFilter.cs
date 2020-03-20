using CoreApi.Models;
using Jose;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CoreApi.Common
{
    public class TokenActionFilter : IAuthorizationFilter
    {
        IConfiguration _configuration;
        public TokenActionFilter(IConfiguration configuration) {
            _configuration = configuration;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context.RouteData.Values["controller"].ToString() == "ChkLogin")
                return;
            var Authorization = context.HttpContext.Request.Headers["Authorization"];
            if (Authorization.Count == 0 || context.HttpContext.Request.Scheme != "Bearer")
            {
                context.HttpContext.Response.StatusCode = 401;
            }
            else
            {
                try
                {
                    string JwtKey =_configuration.GetValue<string>("JwtKey");
                     var jwtObject = Jose.JWT.Decode<BaseHeaderData>(
                        context.HttpContext.Request.Headers["Authorization"],
                        Encoding.UTF8.GetBytes(JwtKey),
                        JwsAlgorithm.HS256);
                }
                catch (Exception)
                {
                    context.HttpContext.Response.StatusCode = 401;
                }
            }
        }
    }
}
