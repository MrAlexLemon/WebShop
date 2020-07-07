using System;
using System.Collections.Generic;
using System.Text;
using WebShop.Services.Common.Messages;

namespace WebShop.Services.Products.Application.Messages.Events
{
    public class ProductsReleased : IEvent
    {
        public Guid OrderId { get; set; }
        public IDictionary<Guid, int> Products { get; }

        public ProductsReleased(Guid orderId, IDictionary<Guid, int> products)
        {
            OrderId = orderId;
            Products = products;
        }
    }
}
