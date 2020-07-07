using RabbitMQ.Client;
using RawRabbit;
using RawRabbit.Enrichers.MessageContext;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebShop.Services.Common.Messages;

namespace WebShop.Services.Common.RabbitMq
{
    public class BusPublisher : IBusPublisher
    {
        private readonly IBusClient _busClient;
        private readonly RabbitMqOptions _rabbitMqOptions;

        public BusPublisher(IBusClient busClient, RabbitMqOptions options)
        {
            _busClient = busClient;
            _rabbitMqOptions = options;
        }

        public async Task SendAsync<TCommand>(TCommand command, ICorrelationContext context)
            where TCommand : ICommand
            => await _busClient.PublishAsync(command, ctx => ctx.UseMessageContext(context));

        public async Task PublishAsync<TEvent>(TEvent @event, ICorrelationContext context)
            where TEvent : IEvent
            => await _busClient.PublishAsync(@event, ctx => ctx.UseMessageContext(context));
    }
}
