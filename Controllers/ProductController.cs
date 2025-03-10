using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebFirstRun.Data;
using WebFirstRun.Entity;
using WebFirstRun.ViewModel.ProductVms;

namespace WebFirstRun.Controllers;
public class ProductController : Controller
{
    private readonly FirstRunDbContext dbContext;
    public ProductController(FirstRunDbContext dbContext)
    {   
        this.dbContext = dbContext;
    }
    
    public async Task<IActionResult> Index()
    {
        var products = await dbContext.Products
            .Include(x => x.Category)
            .OrderBy(x => x.Name)
            .ToListAsync();
        return View(products);
    }

    //Search product
    [HttpPost]
    public async Task<IActionResult> Search(string name)
    {
        var searchProduct = await dbContext.Products
            .Where(x => x.Name.Contains(name))
            .ToListAsync();

        if(searchProduct.Count == 0)
        {
            ViewBag.Message = "No products found.";
            return View(new List<Product>());
        }
        return View(searchProduct);
    }

    // delete a product
    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var product = await dbContext.Products.FindAsync(id);
        if(product == null)
        {
            return NotFound();
        }
        dbContext.Products.Remove(product);
        await dbContext.SaveChangesAsync();

        return RedirectToAction("Index");
    }

    // create product Form
    public async Task<IActionResult> Create()
    {
        var productCategories = await dbContext.ProductCategories
                .OrderBy(x => x.Name) 
                .ToListAsync();

        var vm = new CreateProductVms();
        vm.ProductCategories = productCategories;
        
        return View(vm);
    }



    //Db Handling action
    [HttpPost]
    public async Task<IActionResult> Create(CreateProductVms vms)
    {
        try
        {
            //Check for validation error
            if(!ModelState.IsValid)
            {
                throw new Exception("Product Category Not Found.");
            }
            //Check for name uniqueness
            

            var productCategory = await dbContext.ProductCategories
                .Where(x => x.Id == vms.ProductCategoryId)
                .FirstOrDefaultAsync();

            if(productCategory == null)
            {
                throw new Exception("Product Category not Found.");
            }

            //Create new product
            var product = new Product();
            product.Name = vms.Name;
            product.Category = productCategory;
            product.Description = vms.Description;

            dbContext.Products.Add(product);
            await dbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }   
        catch(Exception e)
        {
            //DO Something
            // Usually send error response to views
            return BadRequest(e.Message);
        }
    }

    //Create Product Form 
    public async Task<IActionResult> Update(int id)
    {
        try
        {
            var productCategories = await dbContext.ProductCategories
                    .OrderBy( x=> x.Name)
                    .ToListAsync();
            
            var product = await dbContext.Products
                    .Where(x => x.Id == id)
                    .FirstOrDefaultAsync();
            if(product == null)
            {
                throw new Exception("Product not found.");
            }

            var vm = new UpdateProductVm();
            vm.ProductCategories = productCategories;

            vm.Name = product.Name;
            vm.ProductCategoryId = product.CategoryId;
            vm.Description = product.Description;

            return View(vm);
        }
        catch(Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    //Db handling action
    [HttpPost]
    public async Task<IActionResult> Update(int id,CreateProductVms vm)
    {
        try
        {
            //Check for validation errors
            if(!ModelState.IsValid)
            {
                return View(vm);
            }

            var productCategory = await dbContext.ProductCategories
                    .Where(x => x.Id == vm.ProductCategoryId)
                    .FirstOrDefaultAsync();
            
            if(productCategory == null)
            {
                throw new Exception("Product Category not found");
            }

            var product = await dbContext.Products
                    .Where(x => x.Id == id )
                    .FirstOrDefaultAsync();

            if(product == null)
            {
                throw new Exception("Product not found");
            }

            product.Name = vm.Name;
            product.Category = productCategory;
            product.Description = vm.Description;

            dbContext.Products.Update(product);

            await dbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        catch(Exception e)
        {
            return BadRequest(e.Message); 
        }
    }
}
