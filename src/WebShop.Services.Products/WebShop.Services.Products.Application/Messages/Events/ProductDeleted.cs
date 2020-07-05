using System;
using System.Collections.Generic;
using System.Text;
using WebShop.Services.Products.Core.Messages;

namespace WebShop.Services.Products.Application.Messages.Events
{
    public class ProductDeleted : IEvent
    {
        public Guid Id { get; }

        public ProductDeleted(Guid id)
        {
            Id = id;
        }
    }
}
