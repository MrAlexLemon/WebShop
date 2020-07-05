using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using WebShop.Services.Products.Core.Exceptions;

namespace WebShop.Services.Products.Core.Messages
{
    public interface IBusSubscriber
    {
        IBusSubscriber SubscribeCommand<TCommand>(string @namespace = null, string queueName = null,
            Func<TCommand, DomainException, IRejectedEvent> onError = null)
            where TCommand : ICommand;

        IBusSubscriber SubscribeEvent<TEvent>(string @namespace = null, string queueName = null,
           Func<TEvent, DomainException, IRejectedEvent> onError = null)
           where TEvent : IEvent;
    }
}
