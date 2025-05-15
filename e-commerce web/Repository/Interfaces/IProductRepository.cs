using e_commerce_web.Models.Domain;

namespace e_commerce_web.Repository.Interfaces
{
    public interface IProductRepository
    {
        Task<Product> CreateAsync(Product product);
        Task<bool> DeleteAsync(Guid id);
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product?> GetProductByIdAsync(Guid productId);

        Task<Product?> UpdateAsync(Guid id, Product product);
    }
}
