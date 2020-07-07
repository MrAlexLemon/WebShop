using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using WebShop.Services.Common.Messages;
using WebShop.Services.Products.Application.Responses;

namespace WebShop.Services.Products.Application.Messages.Commands
{
    public class ReserveProductsCommand : IRequest<ReserveProductsResponse>, ICommand
    {
        public Guid OrderId { get; set; }
        public IDictionary<Guid, int> Products { get; }

        public ReserveProductsCommand(Guid orderId, IDictionary<Guid, int> products)
        {
            OrderId = orderId;
            Products = products;
        }
    }
}
