using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebFirstRun.Entity;

[Table("product_category")]
public class ProductCategory
{
    [Column("id")]//Configuration
    [Key]
    public int Id { get; set;}
    public string Name{ get; set;}
    public bool IsActive{ get; set;}
    public DateTime? CreatedAt{ get; set;}
}
