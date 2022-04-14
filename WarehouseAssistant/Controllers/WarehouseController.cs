using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WarehouseAssistant.Models;

namespace WarehouseAssistant.Controllers
{
    public class WarehouseController : Controller
    {
        private readonly AppContext _db;

        public WarehouseController(AppContext context)
        {
            _db = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var _warehouses = _db.Warehouses;
            return View(_warehouses);
        }

        [HttpPost]
        public IActionResult Index(string nameWarehouse)
        {
            _db.Warehouses.Add(new Warehouse { Name = nameWarehouse });
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int? id)
        {
            if (id != null)
            {
                var _warehouses = await _db.Warehouses.FirstOrDefaultAsync(t => t.Id == id);
                if (_warehouses != null)
                    return View(_warehouses);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                var _warehouses = new Warehouse { Id = id.Value };
                _db.Entry(_warehouses).State = EntityState.Deleted;
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return NotFound();
        }
    }
}
