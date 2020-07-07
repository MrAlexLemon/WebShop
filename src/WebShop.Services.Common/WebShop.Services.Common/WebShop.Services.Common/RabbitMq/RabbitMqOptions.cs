using RawRabbit.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebShop.Services.Common.RabbitMq
{
    public class RabbitMqOptions : RawRabbitConfiguration
    {
        public string Namespace { get; set; }
        public int Retries { get; set; }
        public int RetryInterval { get; set; }
    }
}
