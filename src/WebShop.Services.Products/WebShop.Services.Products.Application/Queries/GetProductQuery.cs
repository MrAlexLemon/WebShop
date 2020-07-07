using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using WebShop.Services.Products.Application.Responses;

namespace WebShop.Services.Products.Application.Queries
{
    public class GetProductQuery : IRequest<ProductResponse>
    {
        public Guid Id { get; set; }
    }
}
