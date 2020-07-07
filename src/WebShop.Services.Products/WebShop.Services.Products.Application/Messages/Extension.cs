using Autofac;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace WebShop.Services.Products.Application.Messages
{
    public static class Extension
    {
        public static void AddMediatr(this ContainerBuilder builder)
        {
            builder
                .RegisterAssemblyTypes(typeof(IRequest<>).Assembly)
                .Where(t => t.IsClosedTypeOf(typeof(IRequest<>)))
                .AsImplementedInterfaces();

            builder
                .RegisterAssemblyTypes(typeof(IRequestHandler<>).Assembly)
                .Where(t => t.IsClosedTypeOf(typeof(IRequestHandler<>)))
                .AsImplementedInterfaces();
        }

        public static IServiceCollection AddMediatorHandlers(this IServiceCollection services, Assembly assembly)
        {

            assembly = Assembly.GetAssembly(typeof(WebShop.Services.Products.Application.Messages.Commands.CreateProductCommand));
            var classTypes = assembly.ExportedTypes.Select(t => t.GetTypeInfo()).Where(t => t.IsClass && !t.IsAbstract);

            foreach (var type in classTypes)
            {
                var interfaces = type.ImplementedInterfaces.Select(i => i.GetTypeInfo());

                foreach (var handlerType in interfaces.Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRequestHandler<,>)))
                {
                    //services.AddTransient(handlerType.AsType(), type.AsType());
                    services.AddTransient(typeof(IRequestHandler<,>), type.AsType());
                    var temp = handlerType.GenericTypeArguments[0].FullName;
                    var temp1 = handlerType.GenericTypeArguments[1].FullName;
                    var t = handlerType.AsType().GenericTypeArguments[0].FullName;
                    var t1 = handlerType.AsType().GenericTypeArguments[1].FullName;
                    //services.AddTransient(typeof(IRequestHandler<temp, handlerType.GenericTypeArguments[1]>), type.AsType());
                    Console.WriteLine(handlerType.GenericTypeArguments[0] + "   " + handlerType.GenericTypeArguments[1]/*handlerType.AsType()*/ + "   " + type.AsType());
                }

                /*foreach (var handlerType in interfaces.Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IAsyncRequestHandler<,>)))
                {
                    services.AddTransient(handlerType.AsType(), type.AsType());
                }*/
            }

            return services;
        }

    }
}
