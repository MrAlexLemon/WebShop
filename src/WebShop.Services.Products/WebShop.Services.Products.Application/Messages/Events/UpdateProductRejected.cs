using System;
using System.Collections.Generic;
using System.Text;
using WebShop.Services.Products.Core.Messages;

namespace WebShop.Services.Products.Application.Messages.Events
{
    public class UpdateProductRejected : IRejectedEvent
    {
        public Guid Id { get; }
        public string Reason { get; }
        public string Code { get; }

        public UpdateProductRejected(Guid id, string reason, string code)
        {
            Id = id;
            Reason = reason;
            Code = code;
        }
    }
}
