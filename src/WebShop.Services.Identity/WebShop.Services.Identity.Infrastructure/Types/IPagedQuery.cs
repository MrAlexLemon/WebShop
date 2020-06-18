using System;
using System.Collections.Generic;
using System.Text;
using WebShop.Services.Identity.Application.Messages;

namespace WebShop.Services.Identity.Infrastructure.Types
{
    public interface IPagedQuery : IQuery
    {
        int Page { get; }
        int Results { get; }
        string OrderBy { get; }
        string SortOrder { get; }
    }
}
