using FluentValidation.Validators;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebShop.Services.Products.Application.Messages;
using WebShop.Services.Products.Application.Messages.Commands;
using WebShop.Services.Products.Application.Messages.Events;
using WebShop.Services.Products.Application.Responses;
using WebShop.Services.Products.Core.Messages;
using WebShop.Services.Products.Core.Repositories;

namespace WebShop.Services.Products.Application.Handlers
{
    public class ReleaseProductsHandler : IRequestHandler<ReleaseProductsCommand, ReleaseProductsResponse>, ICommandHandler<ReleaseProductsCommand>
    {
        private readonly IBusPublisher _busPublisher;
        private readonly IProductRepository _productRepository;
        private readonly ILogger<ReleaseProductsHandler> _logger;

        public ReleaseProductsHandler(IBusPublisher busPublisher,
            IProductRepository productsRepository,
            ILogger<ReleaseProductsHandler> logger)
        {
            _busPublisher = busPublisher;
            _productRepository = productsRepository;
            _logger = logger;
        }

        public async Task<ReleaseProductsResponse> Handle(ReleaseProductsCommand command, CancellationToken cancellationToken)
        {
            await DoHandlerLogicAsync(command, CorrelationContext.Empty);

            return new ReleaseProductsResponse(command.OrderId, command.Products);
        }

        public async Task HandleAsync(ReleaseProductsCommand command, ICorrelationContext context)
        {
            await DoHandlerLogicAsync(command, context);
        }

        private async Task DoHandlerLogicAsync(ReleaseProductsCommand command, ICorrelationContext context)
        {
            foreach ((Guid productId, int quantity) in command.Products)
            {
                _logger.LogInformation($"Releasing a product: '{productId}' ({quantity})");
                var product = await _productRepository.GetAsync(productId);
                if (product == null)
                {
                    _logger.LogInformation($"Product was not found: '{productId}' (can't release).");

                    continue;
                }

                product.SetQuantity(product.Quantity + quantity);
                await _productRepository.UpdateAsync(product);
                _logger.LogInformation($"Released a product: '{productId}' ({quantity})");
            }

            await _busPublisher.PublishAsync(new ProductsReleased(command.OrderId,
                command.Products), context);
        }
    }
}
