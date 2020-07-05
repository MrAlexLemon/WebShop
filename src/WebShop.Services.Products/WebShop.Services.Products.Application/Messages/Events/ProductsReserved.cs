﻿using System;
using System.Collections.Generic;
using System.Text;
using WebShop.Services.Products.Core.Messages;

namespace WebShop.Services.Products.Application.Messages.Events
{
    public class ProductsReserved : IEvent
    {
        public Guid OrderId { get; set; }
        public IDictionary<Guid, int> Products { get; }

        public ProductsReserved(Guid orderId, IDictionary<Guid, int> products)
        {
            OrderId = orderId;
            Products = products;
        }
    }
}
