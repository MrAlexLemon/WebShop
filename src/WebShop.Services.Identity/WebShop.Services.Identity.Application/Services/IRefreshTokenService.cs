using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebShop.Services.Identity.Application.Dto;

namespace WebShop.Services.Identity.Application.Services
{
    public interface IRefreshTokenService
    {
        Task AddAsync(Guid userId);
        Task<JsonWebToken> CreateAccessTokenAsync(string refreshToken);
        Task RevokeAsync(string refreshToken, Guid userId);

        /*Task<string> CreateAsync(Guid userId);
        Task RevokeAsync(string refreshToken);
        Task<AuthDto> UseAsync(string refreshToken);*/
    }
}
