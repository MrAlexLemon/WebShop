using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Services.Identity.Application.Services
{
    public interface IClaimsProvider
    {
        Task<IDictionary<string, string>> GetAsync(Guid userId);
    }
}
