using System;
using System.Collections.Generic;
using System.Text;
using WebShop.Services.Identity.Application.Messages;

namespace WebShop.Services.Identity.Application.Events
{
    public class PasswordChanged : IEvent
    {
        public Guid UserId { get; }

        public PasswordChanged(Guid userId)
        {
            UserId = userId;
        }
    }
}
