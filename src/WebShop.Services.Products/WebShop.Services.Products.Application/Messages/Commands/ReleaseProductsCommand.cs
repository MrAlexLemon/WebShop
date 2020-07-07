using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using WebShop.Services.Products.Application.Responses;
using WebShop.Services.Common.Messages;

namespace WebShop.Services.Products.Application.Messages.Commands
{
    public class ReleaseProductsCommand : IRequest<ReleaseProductsResponse>, ICommand
    {
        public Guid OrderId { get; set; }
        public IDictionary<Guid, int> Products { get; }

        public ReleaseProductsCommand(Guid orderId, IDictionary<Guid, int> products)
        {
            OrderId = orderId;
            Products = products;
        }
    }
}
