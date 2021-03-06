﻿using System;
using System.Collections.Generic;
using System.Text;
using WebShop.Services.Common.Messages;

namespace WebShop.Services.Identity.Application.Events
{
    public class SignedIn : IEvent
    {
        public Guid UserId { get; }

        public SignedIn(Guid userId)
        {
            UserId = userId;
        }
    }
}
