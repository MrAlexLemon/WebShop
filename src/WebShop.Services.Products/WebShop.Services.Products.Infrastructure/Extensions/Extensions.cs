﻿using WebShop.Services.Products.Core.Entities.Base;
using WebShop.Services.Products.Core.Repositories;
using Autofac;
using WebShop.Services.Products.Infrastructure.Repositories;
using MongoDB.Driver;
using Microsoft.Extensions.Configuration;
using System.Linq.Expressions;
using System;
using System.Reflection;
using System.Linq;

namespace WebShop.Services.Products.Infrastructure.Extensions
{
    public static class Extensions
    {                
        private static readonly string SectionName = "mongo";

        public static void AddMongo(this ContainerBuilder builder)
        {
            builder.Register(context =>
            {
                var configuration = context.Resolve<IConfiguration>();
                var options = configuration.GetOptions<MongoDbOptions>(SectionName);

                return options;
            }).SingleInstance();

            builder.Register(context =>
            {
                var options = context.Resolve<MongoDbOptions>();

                return new MongoClient(options.ConnectionString);
            }).SingleInstance();

            builder.Register(context =>
            {
                var options = context.Resolve<MongoDbOptions>();
                var client = context.Resolve<MongoClient>();
                return client.GetDatabase(options.Database);

            }).InstancePerLifetimeScope();
        }

        public static void AddMongoRepository<TEntity>(this ContainerBuilder builder, string collectionName)
            where TEntity : IIdentifiable
            => builder.Register(ctx => new MongoRepository<TEntity>(ctx.Resolve<IMongoDatabase>(), collectionName))
                .As<IMongoRepository<TEntity>>()
                .InstancePerLifetimeScope();

        public static TModel GetOptions<TModel>(this IConfiguration configuration, string section) where TModel : new()
        {
            var model = new TModel();
            configuration.GetSection(section).Bind(model);

            return model;
        }
    }
}
