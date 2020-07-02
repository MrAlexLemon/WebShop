using System;
using System.Collections.Generic;
using System.Text;

namespace WebShop.Services.Products.Infrastructure.Extensions
{
    public class MongoDbOptions
    {
        public string ConnectionString { get; set; }
        public string Database { get; set; }
    }
}
