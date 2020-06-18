﻿using System;
using System.Collections.Generic;
using System.Text;
using WebShop.Services.Identity.Application.Messages;

namespace WebShop.Services.Identity.Application.Events
{
    public class AccessTokenRefreshed : IEvent
    {
        public Guid UserId { get; }

        public AccessTokenRefreshed(Guid userId)
        {
            UserId = userId;
        }
    }
}
