using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Services.Products.Infrastructure.Utils
{
    public interface IInitializer
    {
        Task InitializeAsync();
    }
}
