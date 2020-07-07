using System;
using System.Collections.Generic;
using System.Text;

namespace WebShop.Services.Common.Entities.Base
{
    public interface IIdentifiable
    {
        Guid Id { get; }
    }
}
