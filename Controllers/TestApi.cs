using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebFirstRun.Data;
using WebFirstRun.Entity;

namespace WebFirstRun.Controllers
{
    [ApiController]
    public class TestApi : ControllerBase
    {
        private readonly FirstRunDbContext dbContext;
        public TestApi(FirstRunDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet("/api/product-category/seed")]
        public async Task<IActionResult> SeedProductCategory()
        {
            var productCategory = new ProductCategory
            {
                Name = "Test Category",
                IsActive = true,
                CreatedAt = System.DateTime.UtcNow,
            };
            dbContext.ProductCategories.Add(productCategory); // Marked as to be added
            // dbContext.SaveChanges();

            await dbContext.SaveChangesAsync(); // Using the async version
            
            return Ok(productCategory);
        }

        //get list
        [HttpGet("/api/product-category/")]

        public async Task<IActionResult> GetProductCategories()
        {
            var productCategory = dbContext.ProductCategories.ToList();

            return Ok(productCategory);
        }

        //Update
        [HttpGet("/api/product-category/update/{id}")]

        public async Task<IActionResult> UpdateProductCategory(int id)
        {
            var productCategory = await dbContext.ProductCategories
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync();

            if(productCategory == null)
            {
                return NotFound();
            }

            productCategory.Name = "Updated Name";
            dbContext.ProductCategories.Update(productCategory); // Product category maked for update

            await dbContext.SaveChangesAsync(); // sends the query to database
            return Ok(productCategory);
        }

        //Foreign Key handling
        [HttpGet("/api/product-category/create-sample-product/{id}")]

        public async Task<IActionResult> CreateSampleProduct(int id)
        {
            var productCategory = await dbContext.ProductCategories
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync();

            if(productCategory == null)
            {
                return NotFound();
            }

            var product = new Product();
            product.Name = "Sample Product";
            product.Description = "Sample Description";
            product.IsActive = true;
            product.CreatedAt = System.DateTime.UtcNow;
            product.Category = productCategory; // Assign the category

            dbContext.Products.Add(product); // Mark for create

            await dbContext.SaveChangesAsync(); // sends the query to database
            return Ok(productCategory);
        }

        // GET Product List
        [HttpGet("/api/products")]
        public async Task<IActionResult> GetProducts()
        {
            var products = await dbContext.Products
                .ToListAsync();
            return Ok(products);
        }
    }
}
