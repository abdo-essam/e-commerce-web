using e_commerce_web.Models.Domain;

namespace e_commerce_web.Repository.Interfaces
{
    public interface ICategoryRepository
    {
        Task<Category>CreateAsync(Category category);
        Task<Category?> GetCategoryByIdAsync(Guid categoryId);
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category?> UpdateAsync(Guid id,Category category);
        Task<bool> DeleteAsync(Guid id);
    }
}
