using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebShop.Services.Products.Application.Queries;
using WebShop.Services.Products.Application.Responses;
using WebShop.Services.Products.Core.Repositories;
using WebShop.Services.Products.Core.Utils;

namespace WebShop.Services.Products.Application.Handlers
{
    public sealed class BrowseProductsHandler : IRequestHandler<BrowseProductsQuery, PagedResult<ProductResponse>>
    {
        private readonly IProductRepository _productRepository;

        public BrowseProductsHandler(IProductRepository productsRepository)
            => _productRepository = productsRepository;

        public async Task<PagedResult<ProductResponse>> Handle(BrowseProductsQuery query, CancellationToken cancellationToken)
        {
            BrowseProducts tempQuery = new BrowseProducts { PriceFrom = query.PriceFrom, PriceTo  = query.PriceTo, Page = query.Page, Results = query.Results, OrderBy = query.OrderBy, SortOrder = query.SortOrder };
            var pagedResult = await _productRepository.BrowseAsync(tempQuery);
            var products = pagedResult.Items.Select(p => new ProductResponse
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Vendor = p.Vendor,
                Price = p.Price,
                Quantity = p.Quantity
            }).ToList();

            return PagedResult<ProductResponse>.From(pagedResult, products);
        }
    }
}
