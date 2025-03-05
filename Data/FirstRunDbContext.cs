using System;
using Microsoft.EntityFrameworkCore;

namespace WebFirstRun.Data;

public class FirstRunDbContext: DbContext
{
    public FirstRunDbContext(DbContextOptions<FirstRunDbContext> options) : base(options)
    {

    }
    public DbSet<Entity.ProductCategory>ProductCategories { get; set; }
    public DbSet<Entity.Product> Products { get; set;}
}
