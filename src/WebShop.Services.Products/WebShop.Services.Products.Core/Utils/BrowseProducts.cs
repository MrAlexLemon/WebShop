using System;
using System.Collections.Generic;
using System.Text;

namespace WebShop.Services.Products.Core.Utils
{
    public class BrowseProducts : PagedQueryBase
    {
        public decimal PriceFrom { get; set; }
        public decimal PriceTo { get; set; } = decimal.MaxValue;
    }
}
