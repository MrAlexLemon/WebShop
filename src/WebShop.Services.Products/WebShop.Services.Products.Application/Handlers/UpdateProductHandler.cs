using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebShop.Services.Common.Exceptions;
using WebShop.Services.Common.Messages;
using WebShop.Services.Common.RabbitMq;
using WebShop.Services.Products.Application.Messages;
using WebShop.Services.Products.Application.Messages.Commands;
using WebShop.Services.Products.Application.Messages.Events;
using WebShop.Services.Products.Application.Responses;
using WebShop.Services.Products.Core.Repositories;

namespace WebShop.Services.Products.Application.Handlers
{
    public sealed class UpdateProductHandler : IRequestHandler<UpdateProductCommand,UpdateProductResponse>,ICommandHandler<UpdateProductCommand>
    {
        private readonly IProductRepository _productRepository;
        private readonly IBusPublisher _busPublisher;

        public UpdateProductHandler(
            IProductRepository productsRepository,
            IBusPublisher busPublisher)
        {
            _productRepository = productsRepository;
            _busPublisher = busPublisher;
        }

        public async Task<UpdateProductResponse> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            await HandleAsync(command, CorrelationContext.Empty);

            return new UpdateProductResponse(command.Id, command.Name, command.Description, command.Vendor, command.Price, command.Quantity);
        }

        public async Task HandleAsync(UpdateProductCommand command, ICorrelationContext context)
        {
            await DoHandlerLogicAsync(command, context);
        }

        private async Task DoHandlerLogicAsync(UpdateProductCommand command, ICorrelationContext context)
        {
            var product = await _productRepository.GetAsync(command.Id);
            if (product == null)
            {
                throw new DomainException("product_not_found",
                    $"Product with id: '{command.Id}' was not found.");
            }

            product.SetName(command.Name);
            product.SetDescription(command.Description);
            product.SetVendor(command.Vendor);
            product.SetPrice(command.Price);
            product.SetQuantity(command.Quantity);
            await _productRepository.UpdateAsync(product);
            await _busPublisher.PublishAsync(new ProductUpdated(command.Id, command.Name,
                command.Description, command.Vendor, command.Price, command.Quantity), context);
        }
    }
}
