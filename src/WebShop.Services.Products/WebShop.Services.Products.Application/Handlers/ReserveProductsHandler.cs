using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebShop.Services.Common.Messages;
using WebShop.Services.Common.RabbitMq;
using WebShop.Services.Products.Application.Messages;
using WebShop.Services.Products.Application.Messages.Commands;
using WebShop.Services.Products.Application.Messages.Events;
using WebShop.Services.Products.Application.Responses;
using WebShop.Services.Products.Core.Repositories;

namespace WebShop.Services.Products.Application.Handlers
{
    public class ReserveProductsHandler : IRequestHandler<ReserveProductsCommand, ReserveProductsResponse>, ICommandHandler<ReserveProductsCommand>
    {
        private readonly IBusPublisher _busPublisher;
        private readonly IProductRepository _productRepository;
        private readonly ILogger<ReserveProductsHandler> _logger;

        public ReserveProductsHandler(IBusPublisher busPublisher,
            IProductRepository productsRepository,
            ILogger<ReserveProductsHandler> logger)
        {
            _busPublisher = busPublisher;
            _productRepository = productsRepository;
            _logger = logger;
        }

        public async Task<ReserveProductsResponse> Handle(ReserveProductsCommand command, CancellationToken cancellationToken)
        {
            await DoHandlerLogicAsync(command, CorrelationContext.Empty);

            return new ReserveProductsResponse(command.OrderId, command.Products);
        }

        public async Task HandleAsync(ReserveProductsCommand command, ICorrelationContext context)
        {
            await DoHandlerLogicAsync(command, context);
        }

        private async Task DoHandlerLogicAsync(ReserveProductsCommand command, ICorrelationContext context)
        {
            foreach ((Guid productId, int quantity) in command.Products)
            {
                _logger.LogInformation($"Reserving a product: '{productId}' ({quantity})");
                var product = await _productRepository.GetAsync(productId);
                if (product == null)
                {
                    _logger.LogInformation($"Product was not found: '{productId}' (can't reserve).");

                    continue;
                }

                product.SetQuantity(product.Quantity - quantity);
                await _productRepository.UpdateAsync(product);
                _logger.LogInformation($"Reserved a product: '{productId}' ({quantity})");
            }

            await _busPublisher.PublishAsync(new ProductsReserved(command.OrderId,
                command.Products), context);
        }
    }
}
