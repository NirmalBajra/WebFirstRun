using System;
using WebFirstRun.Entity;

namespace WebFirstRun.ViewModel.ProductVms;

public class ProductVm
{
    public string Name{ get;set;}
    public string Description { get; set;}
    public int CategoryId { get;set;}
    
    public List<Product> Products;
}
