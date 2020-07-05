using System;
using System.Collections.Generic;
using System.Text;

namespace WebShop.Services.Products.Application.Responses
{
    public class DeleteProductResponse
    {
        public Guid Id { get; }

        public DeleteProductResponse(Guid id)
        {
            Id = id;
        }
    }

}
