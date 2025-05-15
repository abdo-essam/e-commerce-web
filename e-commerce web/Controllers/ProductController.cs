using e_commerce_web.Models.Domain;
using e_commerce_web.Models.Dto;
using e_commerce_web.Repository.Interfaces;
using e_commerce_web.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace e_commerce_web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository productRepository;

        public ProductController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        [HttpPost("Add Product")]
        public async Task<IActionResult> AddProduct([FromBody] AddProductRequestDto addProduct)
        {
            var productionDomain = new Product()
            {
                Name = addProduct.Name,
                Description = addProduct.Description,
                Price = addProduct.Price,
                StockQuantity = addProduct.StockQuantity,
                CategoryId = addProduct.CategoryId

            };
            var createdProduct = await productRepository.CreateAsync(productionDomain);
            if (createdProduct == null)
            {
                return BadRequest("Product creation failed");
            }

            return Ok(createdProduct);

        }

        [HttpGet("All Products")]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await productRepository.GetAllAsync();
            if (products == null || !products.Any())
            {
                return NotFound();
            }
            var productDtos = products.Select(product => new GetProductResponseDto
            {
                Id = product.ProductId,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                StockQuantity = product.StockQuantity,
                CategoryName = product.Category?.Name
            }).ToList();
            return Ok(productDtos);
        }


        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetProduct([FromRoute] Guid id)
        {
            var productDomain = await productRepository.GetProductByIdAsync(id);
            if (productDomain == null)
            {
                return NotFound();
            }
            var productDto = new GetProductResponseDto
            {
                Id = productDomain.ProductId,
                Name = productDomain.Name,
                Description = productDomain.Description,
                Price = productDomain.Price,
                StockQuantity = productDomain.StockQuantity,
                CategoryName = productDomain.Category?.Name
            };
            return Ok(productDto);
        }
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateProduct([FromRoute] Guid id, UpdateProductRequestDto updateProduct)
        {
            var productToUpdate = new Product
            {
                Name = updateProduct.Name,
                Description = updateProduct.Description,
                Price = updateProduct.Price,
                StockQuantity = updateProduct.StockQuantity,
            };

            productToUpdate = await productRepository.UpdateAsync(id, productToUpdate);
            if (productToUpdate == null)
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] Guid id)
        {
            var isDeleted = await productRepository.DeleteAsync(id);
            if (!isDeleted)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
