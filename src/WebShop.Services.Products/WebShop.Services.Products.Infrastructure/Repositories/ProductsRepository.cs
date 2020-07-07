using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebShop.Services.Common.Mongo;
using WebShop.Services.Common.Utils;
using WebShop.Services.Products.Core.Entities;
using WebShop.Services.Products.Core.Repositories;

namespace WebShop.Services.Products.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IMongoRepository<Product> _repository;

        public ProductRepository(IMongoRepository<Product> repository)
        {
            _repository = repository;
        }

        public async Task<Product> GetAsync(Guid id)
            => await _repository.GetAsync(id);

        public async Task<bool> ExistsAsync(Guid id)
            => await _repository.ExistsAsync(p => p.Id == id);

        public async Task<bool> ExistsAsync(string name)
            => await _repository.ExistsAsync(p => p.Name == name.ToLowerInvariant());

        public async Task<PagedResult<Product>> BrowseAsync(BrowseProducts query)
            => await _repository.BrowseAsync(p =>
                p.Price >= query.PriceFrom && p.Price <= query.PriceTo, query);

        public async Task AddAsync(Product product)
            => await _repository.AddAsync(product);

        public async Task UpdateAsync(Product product)
            => await _repository.UpdateAsync(product);

        public async Task DeleteAsync(Guid id)
            => await _repository.DeleteAsync(id);
    }
}
