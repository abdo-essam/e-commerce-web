using e_commerce_web.Data;
using e_commerce_web.Models.Domain;
using Microsoft.EntityFrameworkCore;

using e_commerce_web.Repository.Interfaces;

namespace e_commerce_web.Repository
{
    public class SqlProductRepository : IProductRepository
    {
        private readonly ApplicationDbcontext dbcontext;

        public SqlProductRepository(ApplicationDbcontext dbcontext)
        {
            this.dbcontext = dbcontext;
        }
        public async Task<Product> CreateAsync(Product product)
        {
            await dbcontext.Products.AddAsync(product);
            await dbcontext.SaveChangesAsync();
            return product;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var existingProduct =await dbcontext.Products.FirstOrDefaultAsync(c => c.ProductId == id);
            if (existingProduct == null)
            {
                return false;
            }
            dbcontext.Products.Remove(existingProduct);
            await dbcontext.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await dbcontext.Products.Include(u => u.Category).ToListAsync();


        }

        public async Task<Product?> GetProductByIdAsync(Guid productId)
        {
            var existingProduct = await dbcontext.
                Products.Include(u => u.Category).FirstOrDefaultAsync(u => u.ProductId == productId);
            if (existingProduct == null)
            {
                return null;
            }
            return existingProduct;
        }

        public async Task<Product?> UpdateAsync(Guid id, Product product)
        {
            var existingProduct = await dbcontext.Products.FindAsync(id);
            if (existingProduct == null)
            {
                return null;
            }
            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;
            existingProduct.Price = product.Price;
            existingProduct.StockQuantity = product.StockQuantity;
            await dbcontext.SaveChangesAsync();
            return existingProduct;

        }

    }

}
