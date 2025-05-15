using e_commerce_web.Data;
using e_commerce_web.Models.Domain;
using e_commerce_web.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace e_commerce_web.Repository
{
    public class SqlCategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbcontext _dbContext;
        public SqlCategoryRepository(ApplicationDbcontext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Category> CreateAsync(Category category)
        {
            await _dbContext.Categories.AddAsync(category);
            await _dbContext.SaveChangesAsync();
            return category;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var existingCategory =await _dbContext.Categories.FirstOrDefaultAsync(c => c.CategoryId == id);
            if (existingCategory == null)
            {
                return false;
            }
            _dbContext.Categories.Remove(existingCategory);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {

            return await _dbContext.Categories.Include(u=>u.Products).ToListAsync();

           

        }

        public async Task<Category?> GetCategoryByIdAsync(Guid categoryId)
        {
            var existingCategory = await _dbContext.Categories.Include(u => u.Products).FirstOrDefaultAsync(u=>u.CategoryId==categoryId);
            if (existingCategory == null)
            {
                return null;
            }
            return existingCategory;
        }

        public async Task<Category?> UpdateAsync(Guid id, Category category)
        {
            var existing=await _dbContext.Categories.FirstOrDefaultAsync(c => c.CategoryId == id);
            if (existing == null)
            {
                return null;
            }
            existing.Name = category.Name;
            await _dbContext.SaveChangesAsync();
            return existing;
        }
    }
}
