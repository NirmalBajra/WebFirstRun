using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebFirstRun.Entity;

namespace WebFirstRun.ViewModel.ProductVms;

public class UpdateProductVm
{
    public string Name {get; set;}
    public int ProductCategoryId {get;set;}
    public string Description {get;set;}

    public SelectList ProductCategoriesSelectList()
        => new SelectList(

            //List of items
            ProductCategories,
            nameof(ProductCategory.Id),
            //Display text
            nameof(ProductCategory.Name),
            ProductCategoryId
        );
        public List<ProductCategory> ProductCategories;
}
