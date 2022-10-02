using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MVC_TemplateApp.Models.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_TemplateApp.Models;

public class Product:BaseEntity
{
    public Product()
    {
        this.ProductPhotos = new HashSet<ProductPhoto>();
    }
    public string Name { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    [Column(TypeName = "decimal(18,2)")]
    public decimal StartPrice { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }
    public double Rate { get; set; }
    public virtual ICollection<ProductPhoto> ProductPhotos { get; set; }
}

 
 