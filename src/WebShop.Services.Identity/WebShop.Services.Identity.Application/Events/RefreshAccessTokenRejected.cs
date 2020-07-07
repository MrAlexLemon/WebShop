﻿using System;
using System.Collections.Generic;
using System.Text;
using WebShop.Services.Common.Messages;

namespace WebShop.Services.Identity.Application.Events
{
    public class RefreshAccessTokenRejected : IRejectedEvent
    {
        public Guid UserId { get; }
        public string Reason { get; }
        public string Code { get; }

        public RefreshAccessTokenRejected(Guid userId, string reason, string code)
        {
            UserId = userId;
            Reason = reason;
            Code = code;
        }
    }
}
