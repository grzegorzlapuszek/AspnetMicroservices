using Catalog.API.Data;
using Catalog.API.Entities;
using MongoDB.Driver;

namespace Catalog.API.Repositories
{
    public class ProductRepository : IProductRepository
    {

        private readonly ICatalogContext _context;

        public ProductRepository(ICatalogContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            var products = await _context.Products.FindAsync(p=>true);
            return (IEnumerable<Product>)products;
        }
        public async Task<Product> GetProduct(string id)
        {
            return (Product)await _context.Products.FindAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Product>> GetProductByName(string name)
        {
            return (IEnumerable<Product>)await _context.Products.FindAsync(p => p.Name == name);
        }

        public async Task<IEnumerable<Product>> GetProductByCategory(string categoryName)
        {
            return (IEnumerable<Product>)await _context.Products.FindAsync(p => p.Category == categoryName);
        }

        public async Task CreateProduct(Product product)
        {
            await _context.Products.InsertOneAsync(product);
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            var updateResult = await _context.Products.ReplaceOneAsync(filter: p=>p.Id == product.Id, replacement: product);
            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }


        public async Task<bool> DeleteProduct(string id)
        {
            var deleteResult = await _context.Products.DeleteOneAsync(p => p.Id ==id);
            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }
    }
}
