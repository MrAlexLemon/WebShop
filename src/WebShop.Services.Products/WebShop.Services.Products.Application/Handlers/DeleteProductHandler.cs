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
using WebShop.Services.Products.Core.Exceptions;
using WebShop.Services.Products.Core.Messages;
using WebShop.Services.Products.Core.Repositories;

namespace WebShop.Services.Products.Application.Handlers
{
    public sealed class DeleteProductHandler : IRequestHandler<DeleteProductCommand,DeleteProductResponse>, ICommandHandler<DeleteProductCommand>
    {
        private readonly IProductRepository _productRepository;
        private readonly IBusPublisher _busPublisher;

        public DeleteProductHandler(
            IProductRepository productsRepository,
            IBusPublisher busPublisher)
        {
            _productRepository = productsRepository;
            _busPublisher = busPublisher;
        }

        public async Task<DeleteProductResponse> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            await DoHandlerLogicAsync(command, CorrelationContext.Empty);

            return new DeleteProductResponse(command.Id);
        }

        public async Task HandleAsync(DeleteProductCommand command, ICorrelationContext context)
        {
            await DoHandlerLogicAsync(command, context);
        }

        private async Task DoHandlerLogicAsync(DeleteProductCommand command, ICorrelationContext context)
        {
            if (!await _productRepository.ExistsAsync(command.Id))
            {
                throw new DomainException("product_not_found",
                    $"Product with id: '{command.Id}' was not found.");
            }

            await _productRepository.DeleteAsync(command.Id);
            await _busPublisher.PublishAsync(new ProductDeleted(command.Id), context);
        }
    }
}
