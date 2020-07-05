using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using WebShop.Services.Products.Application.Responses;
using WebShop.Services.Products.Core.Messages;

namespace WebShop.Services.Products.Application.Messages.Commands
{
    public class DeleteProductCommand : IRequest<DeleteProductResponse>, ICommand
    {
        public Guid Id { get; }

        public DeleteProductCommand(Guid id)
        {
            Id = id;
        }
    }
}
