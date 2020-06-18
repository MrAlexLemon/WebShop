using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebShop.Services.Identity.Core.Entities;
using WebShop.Services.Identity.Infrastructure.Mongo.Repositories;

namespace WebShop.Services.Identity.Infrastructure.Mongo
{
    public static class Extensions
    {
        public static IApplicationBuilder UseMongo(this IApplicationBuilder builder)
        {
            using (var scope = builder.ApplicationServices.CreateScope())
            {
                var users = scope.ServiceProvider.GetService<IMongoRepository<User>>().Collection;
                var userBuilder = Builders<User>.IndexKeys;
                Task.Run(async () => await users.Indexes.CreateOneAsync(
                    new CreateIndexModel<User>(userBuilder.Ascending(i => i.Email),
                        new CreateIndexOptions
                        {
                            Unique = true
                        })));
            }

            return builder;
        }
    }
}
