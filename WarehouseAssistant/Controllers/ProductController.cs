using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WarehouseAssistant.Models;

namespace WarehouseAssistant.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppContext _db;

        public ProductController(AppContext context)
        {
            _db = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var _products = _db.Products;
            return View(_products);
        }

        [HttpPost]
        public IActionResult Index(string nameProduct)
        {
            _db.Products.Add(new Product { Name = nameProduct });
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int? id)
        {
            if (id != null)
            {
                var _products = await _db.Products.FirstOrDefaultAsync(t => t.Id == id);
                if (_products != null)
                    return View(_products);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                var _products = new Product { Id = id.Value };
                _db.Entry(_products).State = EntityState.Deleted;
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return NotFound();
        }
    }
}
