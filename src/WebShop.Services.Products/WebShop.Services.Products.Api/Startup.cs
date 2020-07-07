using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebShop.Services.Common.ErrorMiddleware;
using WebShop.Services.Common.Mongo;
using WebShop.Services.Common.RabbitMq;
using WebShop.Services.Common.Swagger;
using WebShop.Services.Common.Utils;
using WebShop.Services.Products.Application.Handlers;
using WebShop.Services.Products.Application.Messages;
using WebShop.Services.Products.Application.Messages.Commands;
using WebShop.Services.Products.Application.Messages.Events;
using WebShop.Services.Products.Application.Queries;
using WebShop.Services.Products.Core.Entities;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace WebShop.Services.Products.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Autofac.IContainer Container { get; private set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews()
        .AddNewtonsoftJson();
            services.AddRazorPages();
            services.AddCustomMvc();
            services.AddSwaggerDocs();
            
            services.AddScoped(typeof(WebShop.Services.Products.Core.Repositories.IProductRepository), typeof(WebShop.Services.Products.Infrastructure.Repositories.ProductRepository));
            services.AddMediatR(typeof(CreateProductCommand).GetTypeInfo().Assembly);

            var builder = new ContainerBuilder();
            builder.RegisterAssemblyTypes(Assembly.GetEntryAssembly())
                .AsImplementedInterfaces();
            builder.Populate(services);
            
            builder.AddRabbitMq();
            builder.AddMongo();
            builder.AddMongoRepository<Product>("Products");
            
            Container = builder.Build();
            
            return new AutofacServiceProvider(Container);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env,
            IStartupInitializer startupInitializer)
        {
            if (env.IsDevelopment() || env.EnvironmentName == "local")
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAllForwardedHeaders();
            app.UseSwaggerDocs();
            app.UseErrorHandler();
            app.UseServiceId();


            app.UseRabbitMq()
                .SubscribeCommand<CreateProductCommand>(onError: (c, e) =>
                    new CreateProductRejected(c.Id, e.Message, e.Code))
                .SubscribeCommand<UpdateProductCommand>(onError: (c, e) =>
                    new UpdateProductRejected(c.Id, e.Message, e.Code))
                .SubscribeCommand<DeleteProductCommand>(onError: (c, e) =>
                    new DeleteProductRejected(c.Id, e.Message, e.Code))
                .SubscribeCommand<ReserveProductsCommand>(onError: (c, e) =>
                    new ReserveProductsRejected(c.OrderId, e.Message, e.Code))
                .SubscribeCommand<ReleaseProductsCommand>(onError: (c, e) =>
                   new ReleaseProductsRejected(c.OrderId, e.Message, e.Code));

            startupInitializer.InitializeAsync();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
