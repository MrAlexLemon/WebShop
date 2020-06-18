using System;
using System.Collections.Generic;
using System.Text;

namespace WebShop.Services.Identity.Infrastructure.Types
{
    public class MongoDbOptions
    {
        public string ConnectionString { get; set; }
        public string Database { get; set; }
        public bool Seed { get; set; }
    }
}
