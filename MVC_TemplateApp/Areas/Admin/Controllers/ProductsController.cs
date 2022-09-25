using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC_TemplateApp.Aplication.Abstraction.Services;
using MVC_TemplateApp.Data;
using MVC_TemplateApp.DTOs.Product;
using MVC_TemplateApp.Models;

namespace MVC_TemplateApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private readonly MyContext _context;
        private readonly IFileService _fileService;

        public ProductsController(MyContext context , IFileService fileService)
        {
            _context = context;
            _fileService = fileService;
        }

        // GET: Admin/Products
        public async Task<IActionResult> Index()
        {
              ViewBag.ProductPhotos = _context.ProductPhotos.ToList();
              return View(await _context.Products.ToListAsync());
        }

        // GET: Admin/Products/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Admin/Products/Create
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateProductDto createProductDto)
        {
            if (ModelState.IsValid)
            {
                var product = new Product
                {
                    Name = createProductDto.Name,
                    Title = createProductDto.Title,
                    Description = createProductDto.Description,
                    StartPrice = createProductDto.StartPrice,
                    Price = createProductDto.Price,
                    Rate = createProductDto.Rate,
                    ProductPhotos = new List<ProductPhoto>()
                };

                foreach (var file in createProductDto.ProductPhotos)
                {
                    string photoPath = await _fileService.UploadFile(file, "image/png", 1000000);
                    product.ProductPhotos.Add(new ProductPhoto
                    {
                        Url = photoPath,
                    });
                }

                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(createProductDto);

        }

        // GET: Admin/Products/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,StartPrice,Price,Rate,Id,CreatedDate,ModifiedDate")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Admin/Products/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            var productPhotos = _context.ProductPhotos.Where(p => p.ProductId == id).ToList();
            if (productPhotos != null)
            {
                foreach (var photo in product.ProductPhotos)
                {
                   await _fileService.DeleteFile(photo.Url);
                }
            }
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        // POST: Admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'MyContext.Products'  is null.");
            }
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(Guid id)
        {
          return _context.Products.Any(e => e.Id == id);
        }
    }
}
