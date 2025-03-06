using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebFirstRun.Data;
using WebFirstRun.ViewModel.ProductVms;

namespace WebFirstRun.Controllers;
public class ProductController : Controller
{
    private readonly FirstRunDbContext dbContext;
    public ProductController(FirstRunDbContext dbContext)
    {   
        this.dbContext = dbContext;
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
}
