﻿using System;
using System.Collections.Generic;
using System.Text;

namespace WebShop.Services.Products.Core.Messages
{
    public class RejectedEvent : IRejectedEvent
    {
        public string Reason { get; }
        public string Code { get; }

        public RejectedEvent(string reason, string code)
        {
            Reason = reason;
            Code = code;
        }

        public static IRejectedEvent For(string name)
            => new RejectedEvent($"There was an error when executing: " +
                                 $"{name}", $"{name}_error");
    }
}
