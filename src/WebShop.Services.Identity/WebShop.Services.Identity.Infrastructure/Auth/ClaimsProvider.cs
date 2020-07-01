using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebShop.Services.Identity.Application.Services;

namespace WebShop.Services.Identity.Infrastructure.Auth
{
    public class ClaimsProvider : IClaimsProvider
    {
        public async Task<IDictionary<string, string>> GetAsync(Guid userId)
        {
            return await Task.FromResult(new Dictionary<string, string>());
        }
    }
}
