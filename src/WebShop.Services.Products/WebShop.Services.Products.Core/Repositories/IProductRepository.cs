using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebShop.Services.Products.Core.Entities;
using WebShop.Services.Products.Core.Utils;

namespace WebShop.Services.Products.Core.Repositories
{
    public interface IProductRepository
    {
        Task<Product> GetAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);
        Task<bool> ExistsAsync(string name);
        Task<PagedResult<Product>> BrowseAsync(BrowseProducts query);
        Task AddAsync(Product product);
        Task UpdateAsync(Product product);
        Task DeleteAsync(Guid id);
    }
}
