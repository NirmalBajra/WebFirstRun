using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebFirstRun.Entity;

namespace WebFirstRun.ViewModel.ProductVms;

public class CreateProductVms
{
    //Send from View To Controller
    public string Name{get; set;}
    public int ProductCategoryId {get; set;}
    public string Description {get; set;}

    // Send to View
    public SelectList ProductCategoriesSelectList()
        => new SelectList(
            //List of items
            ProductCategories,
            //value
            nameof(ProductCategory.Id),
            //display text
            nameof(ProductCategory.Name),
            ProductCategoryId
        );
    public List<ProductCategory> ProductCategories;
}
