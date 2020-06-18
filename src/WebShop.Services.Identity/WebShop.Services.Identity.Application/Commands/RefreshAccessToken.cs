using System;
using System.Collections.Generic;
using System.Text;
using WebShop.Services.Identity.Application.Messages;

namespace WebShop.Services.Identity.Application.Commands
{
    public class RefreshAccessToken : ICommand
    {
        public string Token { get; }

        public RefreshAccessToken(string token)
        {
            Token = token;
        }
    }
}
