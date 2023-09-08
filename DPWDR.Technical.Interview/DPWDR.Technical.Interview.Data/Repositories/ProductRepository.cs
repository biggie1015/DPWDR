using DPWDR.Technical.Interview.Data.Context;
using DPWDR.Technical.Interview.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace DPWDR.Technical.Interview.Data.Repositories
{
    public class ProductRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Product>> GetProductsInStockAsync(DateTime? startDate, int? productId)
        {
            var products = await _dbContext.Products.Where(p => p.Stock > 0).ToListAsync();
            if (startDate.HasValue)
            {
                products = products.Where(p => p.Date >= startDate.Value).ToList();
            }

            if (productId.HasValue)
            {
                products = products.Where(p => p.Id == productId.Value).ToList();
            }

            return  products;

        }

        public async Task<bool> ProductExistsAsync(int productId)
        {
            
            return await _dbContext.Products.AnyAsync(p => p.Id == productId);
        }

        public async Task<IEnumerable<Product>> GetFilteredProductsAsync(DateTime? filterDate, int productId)
        {
            var query = _dbContext.Products.Where(p => p.Stock > 0);

            if (filterDate.HasValue)
            {
                query = query.Where(p => p.Date.Date == filterDate.Value.Date);
            }

            if (productId != 0)
            {
                query = query.Where(p => p.Id == productId);
            }

            return await query.ToListAsync();
        }

        public async Task<bool> AddNewProductWithZeroStockAsync(Product product)
        {
            try
            {
                
                var existingProduct = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == product.Id);

                if (existingProduct == null)
                {
                 
                    product.Stock = 0;
                    _dbContext.Products.Add(product);
                    await _dbContext.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> UpdateStockIfZeroAsync(int productId, int newStock)
        {
            try
            {
                
                var product = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == productId && p.Stock == 0);

                if (product != null)
                {
                    product.Stock = newStock;
                    await _dbContext.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
