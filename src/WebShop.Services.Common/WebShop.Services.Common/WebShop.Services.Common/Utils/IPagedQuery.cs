using System;
using System.Collections.Generic;
using System.Text;

namespace WebShop.Services.Common.Utils
{
    public interface IPagedQuery
    {
        int Page { get; }
        int Results { get; }
        string OrderBy { get; }
        string SortOrder { get; }
    }
}
