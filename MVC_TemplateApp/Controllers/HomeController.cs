using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_TemplateApp.Data;
using MVC_TemplateApp.Models;
using System.Diagnostics;

namespace MVC_TemplateApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MyContext _context;
        public HomeController(ILogger<HomeController> logger, MyContext context)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var products = new List<Product>
            {
                new Product {
                    Name = "Fresh organic kiwi",
                    StartPrice = 10.00M,
                    Price = 70.99M,
                    Rate = 4,
                    ProductPhotos = new List<ProductPhoto >{
                        new ProductPhoto
                            {
                                Url = "/images/products/product-image-2-1.jpg"
                            },
                        new ProductPhoto
                            {
                                Url = "/images/products/product-image-2-2.jpg"
                            }
                    }
                } 
            };
             
            return View(_context.Products.Include(i => i.ProductPhotos).ToList());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}