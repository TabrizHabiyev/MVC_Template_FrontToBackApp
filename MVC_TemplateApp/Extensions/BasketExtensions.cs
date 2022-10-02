using Microsoft.EntityFrameworkCore;
using MVC_TemplateApp.Models;

namespace MVC_TemplateApp.Extensions
{
    public static class BasketExtensions
    {

        public static IQueryable<Basket> RetriveBasketWithItems(this IQueryable<Basket> query, string BuyerId)
        {
            return query.Include(i => i.Items)
                .ThenInclude(p => p.Product)
                .Where(b => b.BuyerId == BuyerId);
        }
    }
}
