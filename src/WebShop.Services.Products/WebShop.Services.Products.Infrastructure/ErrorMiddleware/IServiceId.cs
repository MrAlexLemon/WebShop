using System;
using System.Collections.Generic;
using System.Text;

namespace WebShop.Services.Products.Infrastructure.ErrorMiddleware
{
    public interface IServiceId
    {
        string Id { get; }
    }
}
