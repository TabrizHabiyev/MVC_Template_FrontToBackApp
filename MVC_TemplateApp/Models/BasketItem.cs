using MVC_TemplateApp.Models.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_TemplateApp.Models
{
    [Table("BasketItem")]
    public class BasketItem:BaseEntity
    {
        public int Quantity { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public Guid BasketId { get; set; }
        public Basket Basket { get; set; }
    }
}
