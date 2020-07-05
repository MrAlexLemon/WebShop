using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebShop.Services.Products.Application.Messages;
using WebShop.Services.Products.Application.Messages.Commands;
using WebShop.Services.Products.Application.Messages.Events;
using WebShop.Services.Products.Application.Responses;
using WebShop.Services.Products.Core.Entities;
using WebShop.Services.Products.Core.Exceptions;
using WebShop.Services.Products.Core.Messages;
using WebShop.Services.Products.Core.Repositories;

namespace WebShop.Services.Products.Application.Handlers
{
    public sealed class CreateProductHandler : IRequestHandler<CreateProductCommand, CreateProductResponse>, ICommandHandler<CreateProductCommand>
    {
        private readonly IProductRepository _productsRepository;
        private readonly IBusPublisher _busPublisher;


        public CreateProductHandler(
            IProductRepository productsRepository,
            IBusPublisher busPublisher)
        {
            _productsRepository = productsRepository;
            _busPublisher = busPublisher;
        }

        public async Task<CreateProductResponse> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            await DoHandlerLogicAsync(command, CorrelationContext.Empty);

            return new CreateProductResponse(command.Id, command.Name,
                command.Description, command.Vendor, command.Price, command.Quantity);
        }

        public async Task HandleAsync(CreateProductCommand command, ICorrelationContext context)
        {
            await DoHandlerLogicAsync(command, context);
        }

        private async Task DoHandlerLogicAsync(CreateProductCommand command, ICorrelationContext context)
        {
            if (command.Quantity < 0)
            {
                throw new DomainException("invalid_product_quantity",
                    "Product quantity cannot be negative.");
            }

            if (await _productsRepository.ExistsAsync(command.Name))
            {
                throw new DomainException("product_already_exists",
                    $"Product: '{command.Name}' already exists.");
            }

            var product = new Product(command.Id, command.Name,
                command.Description, command.Vendor, command.Price, command.Quantity);
            await _productsRepository.AddAsync(product);
            await _busPublisher.PublishAsync(new ProductCreated(command.Id, command.Name,
                command.Description, command.Vendor, command.Price, command.Quantity), context);
        }
    }
}
