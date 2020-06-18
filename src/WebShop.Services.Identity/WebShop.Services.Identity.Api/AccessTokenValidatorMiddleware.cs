using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WebShop.Services.Identity.Application.Services;

namespace WebShop.Services.Identity.Api
{
    public class AccessTokenValidatorMiddleware : IMiddleware
    {
        private readonly IAccessTokenService _accessTokenService;

        public AccessTokenValidatorMiddleware(IAccessTokenService accessTokenService)
        {
            _accessTokenService = accessTokenService;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if (await _accessTokenService.IsCurrentActiveToken())
            {
                await next(context);

                return;
            }
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
        }
    }
}
