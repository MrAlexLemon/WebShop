using System;
using System.Collections.Generic;
using System.Text;

namespace WebShop.Services.Common.Mongo
{
    public class MongoDbOptions
    {
        public string ConnectionString { get; set; }
        public string Database { get; set; }
    }
}
