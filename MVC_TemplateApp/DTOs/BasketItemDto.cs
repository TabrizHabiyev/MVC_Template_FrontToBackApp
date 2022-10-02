namespace MVC_TemplateApp.DTOs
{
    public class BasketItemDto
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public string Name { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal StartPrice { get; set; }
        public decimal Price { get; set; }
        public double Rate { get; set; }
        public string ProductPhotoUrl { get; set; } = null!;
    }
}
