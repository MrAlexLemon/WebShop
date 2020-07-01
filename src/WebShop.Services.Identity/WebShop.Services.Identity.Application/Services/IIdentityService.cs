using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebShop.Services.Identity.Application.Commands;
using WebShop.Services.Identity.Application.Dto;
using WebShop.Services.Identity.Core.Entities;

namespace WebShop.Services.Identity.Application.Services
{
    public interface IIdentityService
    {
        Task SignUpAsync(Guid id, string email, string password, string role = Role.User);
        Task<JsonWebToken> SignInAsync(string email, string password);
        Task ChangePasswordAsync(Guid userId, string currentPassword, string newPassword);

        /*Task<UserDto> GetAsync(Guid id);
        Task<AuthDto> SignInAsync(SignIn command);
        Task SignUpAsync(SignUp command);*/
    }
}
