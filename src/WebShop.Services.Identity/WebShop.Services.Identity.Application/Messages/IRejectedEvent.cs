using System;
using System.Collections.Generic;
using System.Text;

namespace WebShop.Services.Identity.Application.Messages
{
    public interface IRejectedEvent : IEvent
    {
        string Reason { get; }
        string Code { get; }
    }
}
