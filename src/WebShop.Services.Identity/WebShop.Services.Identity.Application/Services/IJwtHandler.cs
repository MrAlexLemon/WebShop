using System;
using System.Collections.Generic;
using System.Text;
using WebShop.Services.Identity.Application.Dto;

namespace WebShop.Services.Identity.Application.Services
{
    public interface IJwtHandler
    {
        JsonWebToken CreateToken(string userId, string role = null, IDictionary<string, string> claims = null);
        JsonWebTokenPayload GetTokenPayload(string accessToken);
    }
}
