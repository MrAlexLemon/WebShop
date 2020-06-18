using System;
using System.Collections.Generic;
using System.Text;

namespace WebShop.Services.Identity.Application.Messages
{
    public interface IQuery
    {
    }

    public interface IQuery<T> : IQuery
    {
    }
}
