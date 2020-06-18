﻿using System;
using System.Collections.Generic;
using System.Text;
using WebShop.Services.Identity.Application.Messages;

namespace WebShop.Services.Identity.Application.Events
{
    public class AccessTokenRevoked : IEvent
    {
        public Guid UserId { get; }

        public AccessTokenRevoked(Guid userId)
        {
            UserId = userId;
        }
    }
}
