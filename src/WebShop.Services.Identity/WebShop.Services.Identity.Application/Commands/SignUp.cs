﻿using System;
using System.Collections.Generic;
using System.Text;
using WebShop.Services.Identity.Application.Messages;

namespace WebShop.Services.Identity.Application.Commands
{
    public class SignUp : ICommand
    {
        public Guid Id { get; }
        public string Email { get; }
        public string Password { get; }
        public string Role { get; }

        public SignUp(Guid id, string email, string password, string role)
        {
            Id = id;
            Email = email;
            Password = password;
            Role = role;
        }
    }
}
