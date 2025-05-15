using e_commerce_web.Data;
using e_commerce_web.Models.Domain;
using Microsoft.AspNetCore.Identity;

namespace e_commerce_web
{
    public static class DbInitializer
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbcontext>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            // Ensure database is created
            context.Database.EnsureCreated();

            // Check if there are any products
            if (context.Products.Any()) return; // DB has been seeded

            // Seed categories
            var electronics = new Category { CategoryId = Guid.NewGuid(), Name = "Electronics" };
            var clothing = new Category { CategoryId = Guid.NewGuid(), Name = "Clothing" };
            var books = new Category { CategoryId = Guid.NewGuid(), Name = "Books" };

            context.Categories.AddRange(new List<Category> { electronics, clothing, books });
            await context.SaveChangesAsync();

            // Seed products
            var products = new List<Product>
        {
            new Product
            {
                ProductId = Guid.NewGuid(),
                Name = "Smartphone",
                Description = "Latest model with high-end features",
                Price = 799.99M,
                StockQuantity = 50,
                CategoryId = electronics.CategoryId
            },
            new Product
            {
                ProductId = Guid.NewGuid(),
                Name = "Laptop",
                Description = "Powerful laptop for work and gaming",
                Price = 1299.99M,
                StockQuantity = 30,
                CategoryId = electronics.CategoryId
            },
            new Product
            {
                ProductId = Guid.NewGuid(),
                Name = "T-Shirt",
                Description = "Comfortable cotton t-shirt",
                Price = 19.99M,
                StockQuantity = 100,
                CategoryId = clothing.CategoryId
            },
            new Product
            {
                ProductId = Guid.NewGuid(),
                Name = "Programming Book",
                Description = "Learn coding with this comprehensive guide",
                Price = 49.99M,
                StockQuantity = 20,
                CategoryId = books.CategoryId
            }
        };

            context.Products.AddRange(products);
            await context.SaveChangesAsync();

            // Add roles if they don't exist
            if (!await roleManager.RoleExistsAsync("Admin"))
                await roleManager.CreateAsync(new IdentityRole("Admin"));

            if (!await roleManager.RoleExistsAsync("User"))
                await roleManager.CreateAsync(new IdentityRole("User"));

            if (!await roleManager.RoleExistsAsync("Vendor"))
                await roleManager.CreateAsync(new IdentityRole("Vendor"));

            // Add admin user
            var adminUser = new ApplicationUser
            {
                UserName = "admin@example.com",
                Email = "admin@example.com",
                EmailConfirmed = true,
                Address = "Admin Address"
            };

            var result = await userManager.CreateAsync(adminUser, "Admin123!");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }
    }
}
