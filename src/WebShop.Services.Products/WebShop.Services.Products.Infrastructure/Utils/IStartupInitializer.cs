using System;
using System.Collections.Generic;
using System.Text;

namespace WebShop.Services.Products.Infrastructure.Utils
{
    public interface IStartupInitializer : IInitializer
    {
        void AddInitializer(IInitializer initializer);
    }
}
