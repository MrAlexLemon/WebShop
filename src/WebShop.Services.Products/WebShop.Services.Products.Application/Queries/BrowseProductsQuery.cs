using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using WebShop.Services.Common.Utils;
using WebShop.Services.Products.Application.Responses;

namespace WebShop.Services.Products.Application.Queries
{
    public class BrowseProductsQuery : PagedQueryBase, IRequest<PagedResult<ProductResponse>>
    {
        public decimal PriceFrom { get; set; }
        public decimal PriceTo { get; set; } = decimal.MaxValue;
    }
}
