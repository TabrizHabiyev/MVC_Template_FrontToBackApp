using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_TemplateApp.Data;
using MVC_TemplateApp.DTOs;
using MVC_TemplateApp.Models;
using System.Security.Claims;

namespace MVC_TemplateApp.Controllers
{
    public class BasketController : Controller
    {
        private readonly MyContext _context;

        public BasketController(MyContext context)
        {
            _context = context;
        }

        [HttpGet(Name = "GetBasket")]
        public async Task<ActionResult<BasketDto>> GetBasket()
        {
            var basket = await RetrieveBasket(GetBuyerId());
            if (basket == null) return View(new BasketDto() { });
            return View(MapBasketToDto(basket));
        }

        [HttpGet]
        public async Task<ActionResult<BasketDto>> AddItemToBasket([FromQuery] Guid productId, int quantity)
        {
            var basket = await RetrieveBasket(GetBuyerId());
            if (basket == null) basket =await  CreatedBasket();

            var product = await _context.Products.FindAsync(productId);
            if (product == null) return NotFound();

            basket.AddItem(product, quantity);

            var result = await _context.SaveChangesAsync() > 0;
            if (result) return View(nameof(Index));

            return BadRequest(new ProblemDetails { Title = "Problem saving item to basket" });
        }

        [HttpGet]
        public async Task<ActionResult> RemoveBasketItem([FromQuery] Guid productId, int quantity)
        {
            var basket = await RetrieveBasket(GetBuyerId());
            if (basket == null) return NotFound();
            basket.RemoveItem(productId, quantity);
            var result = await _context.SaveChangesAsync() > 0;
            if (result) return View(nameof(GetBasket));
            return BadRequest(new ProblemDetails { Title = "Problem removing item from baskets" });
        }




        private async Task<Basket> RetrieveBasket(string buyerId)
        {
            if (string.IsNullOrEmpty(buyerId))
            {
                Response.Cookies.Delete("buyerId");
                return null;
            }

            return _context.Baskets
                .Include(b => b.Items)
                .ThenInclude(i => i.Product)
                .FirstOrDefault(b => b.BuyerId == buyerId);
        }

        private string GetBuyerId()
        {
            var userId = GetIdentityUserId();
            return userId ?? Request.Cookies["buyerId"];
        }

        private string GetIdentityUserId()
        {
            string userId = null;
            if (User.Identity.IsAuthenticated)
            {
                userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            }

            return userId;
        }

        private async Task<Basket> CreatedBasket()
        {
            var buyerId = GetIdentityUserId();
            if (string.IsNullOrEmpty(buyerId))
            {
                buyerId = Guid.NewGuid().ToString();
                var cookieOptions = new CookieOptions { IsEssential = true, Expires = DateTime.Now.AddDays(30) };
                Response.Cookies.Append("buyerId", buyerId, cookieOptions);
            }

            var basket = new Basket { BuyerId = buyerId };
            await _context.Baskets.AddAsync(basket);
            return basket;
        }

        private BasketDto MapBasketToDto(Basket basket)
        {
            return new BasketDto
            {
                Id = basket.Id,
                BuyerId = basket.BuyerId,
               
                Items = basket.Items.Select(item => new BasketItemDto
                {
                    Description = item.Product.Description,
                    Name = item.Product.Name,
                    ProductId = item.ProductId,
                    Title = item.Product.Title,
                    Price = item.Product.Price,
                    ProductPhotoUrl = _context.ProductPhotos.Where(x => x.Product.Id == item.ProductId && x.isMain == false).Select(x => x.Url).First(),
                    Quantity = item.Quantity,

                }).ToList()
            };
        }
    }
}
