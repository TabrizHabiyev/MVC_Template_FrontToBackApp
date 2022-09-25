using System.ComponentModel.DataAnnotations.Schema;
using MVC_TemplateApp.Models.Common;

namespace MVC_TemplateApp.Models;

public class ProductPhoto :BaseEntity
{
    public Guid ProductId { get; set; }
    public bool isMain { get; set; }
    public Product Product { get; set; } = null!;
    public string Url { get; set; } = null!;
    [NotMapped]
    public override DateTime ModifiedDate { get; set; }
}

 
 