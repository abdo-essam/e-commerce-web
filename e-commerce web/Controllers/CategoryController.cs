using e_commerce_web.Models.Domain;
using e_commerce_web.Models.Dto;
using e_commerce_web.Repository.Interfaces;
using e_commerce_web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace e_commerce_web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }
        [HttpPost("Add Category")]
        public async Task<IActionResult> AddCategory([FromBody] AddCategoryRequestDto categoryRequestDto)
        {

            if (string.IsNullOrEmpty(categoryRequestDto.Name))
            {
                return BadRequest("Category name is required");
            }
            var categoryDomain = new Category()
            {
                Name = categoryRequestDto.Name
            };

            var createdCategory = await categoryRepository.CreateAsync(categoryDomain);

            return Ok();
        }
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetCategoryById([FromRoute] Guid id)
        {
            var category = await categoryRepository.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            var categoryResponseDto = new GetCategoryByIdResponseDto()
            {
                CategoryId = category.CategoryId,
                Name = category.Name,
                Products = category.Products  ?? new List<Product>()
            };
            return Ok(categoryResponseDto);
        }
        [HttpGet("All Categories")]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await categoryRepository.GetAllAsync();
            if (categories == null || !categories.Any())
            {
                return NotFound();
            }
            var categoryResponseDtos = categories.Select(c => new GetCategoriesResponseDto()
            {
                CategoryId = c.CategoryId,
                Name = c.Name,
                Products = c.Products ?? new List<Product>()
            }).ToList();
            return Ok(categoryResponseDtos);
        }
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateCategory([FromRoute] Guid id, [FromBody] UpdateCategoryRequestDto categoryRequestDto)
        {
            var existingCategory = new Category()
            {
                Name = categoryRequestDto.Name

            };
            existingCategory = await categoryRepository.UpdateAsync(id, existingCategory);

            if (existingCategory == null)
            {
                return NotFound();
            }
            return Ok();
        }
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] Guid id)
        {
            var isDeleted = await categoryRepository.DeleteAsync(id);
            if (!isDeleted)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
