using MVC_TemplateApp.Models;

namespace MVC_TemplateApp.DTOs.Product
{
    public class CreateProductDto
    {
    public string Name { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal StartPrice { get; set; } 
    public decimal Price { get; set; }
    public double Rate { get; set; }
    public IFormFileCollection ProductPhotos { get; set; } = null!;
    }
}
