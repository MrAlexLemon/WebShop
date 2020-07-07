using MediatR;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using WebShop.Services.Common.Messages;
using WebShop.Services.Products.Application.Responses;

namespace WebShop.Services.Products.Application.Messages.Commands
{
    public class DeleteProductCommand : IRequest<DeleteProductResponse>, ICommand
    {
        public Guid Id { get; set; }

        public DeleteProductCommand()
        {

        }

        public DeleteProductCommand(Guid id)
        {
            Id = id;
        }
    }
}
