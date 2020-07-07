using System;
using System.Collections.Generic;
using System.Text;

namespace WebShop.Services.Products.Infrastructure.Logging
{
    public class SerilogOptions
    {
        public bool ConsoleEnabled { get; set; }
        public string Level { get; set; }
    }
}
