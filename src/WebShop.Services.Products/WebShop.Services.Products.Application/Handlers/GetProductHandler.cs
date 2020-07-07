using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebShop.Services.Products.Application.Queries;
using WebShop.Services.Products.Application.Responses;
using WebShop.Services.Products.Core.Repositories;

namespace WebShop.Services.Products.Application.Handlers
{
    public sealed class GetProductHandler : IRequestHandler<GetProductQuery, ProductResponse>
    {
        private readonly IProductRepository _productRepository;

        public GetProductHandler(IProductRepository productsRepository)
            => _productRepository = productsRepository;

        public async Task<ProductResponse> Handle(GetProductQuery query, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetAsync(query.Id);

            return product == null ? null : new ProductResponse
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Vendor = product.Vendor,
                Price = product.Price
            };
        }
    }
}
