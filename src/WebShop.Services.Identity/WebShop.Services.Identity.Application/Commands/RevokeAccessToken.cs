using System;
using System.Collections.Generic;
using System.Text;
using WebShop.Services.Identity.Application.Messages;

namespace WebShop.Services.Identity.Application.Commands
{
    public class RevokeAccessToken : ICommand
    {
        public Guid UserId { get; }
        public string Token { get; }

        public RevokeAccessToken(Guid userId, string token)
        {
            UserId = userId;
            Token = token;
        }
    }
}
