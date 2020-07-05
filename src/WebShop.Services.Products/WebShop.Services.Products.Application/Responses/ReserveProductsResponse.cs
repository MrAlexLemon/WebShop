using System;
using System.Collections.Generic;
using System.Text;

namespace WebShop.Services.Products.Application.Responses
{
    public class ReserveProductsResponse
    {
        public Guid OrderId { get; set; }
        public IDictionary<Guid, int> Products { get; }
    }
}
