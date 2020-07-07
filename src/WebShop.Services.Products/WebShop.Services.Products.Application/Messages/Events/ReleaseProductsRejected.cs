using System;
using System.Collections.Generic;
using System.Text;
using WebShop.Services.Common.Messages;

namespace WebShop.Services.Products.Application.Messages.Events
{
    public class ReleaseProductsRejected : IRejectedEvent
    {
        public Guid OrderId { get; }
        public string Reason { get; }
        public string Code { get; }

        public ReleaseProductsRejected(Guid orderId, string reason, string code)
        {
            OrderId = orderId;
            Reason = reason;
            Code = code;
        }
    }
}
