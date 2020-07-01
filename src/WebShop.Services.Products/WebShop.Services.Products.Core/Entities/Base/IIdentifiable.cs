using System;
using System.Collections.Generic;
using System.Text;

namespace WebShop.Services.Products.Core.Entities.Base
{
    public interface IIdentifiable
    {
        Guid Id { get; }
    }
}
