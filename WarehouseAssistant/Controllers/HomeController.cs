using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WarehouseAssistant.Models;

namespace WarehouseAssistant.Controllers
{
    public class HomeController : Controller
    {
        private readonly Models.AppContext _db;

        public HomeController(Models.AppContext context)
        {
            _db = context;
        }

        public async Task<IActionResult> Index()
        {
            var transactions = _db.Transactions
              .Include(x => x.Product)
              .Include(x => x.WarehouseFrom)
              .Include(x => x.WarehouseIn)
              .ToListAsync();
            return View(await transactions);
        }

        public IActionResult Create()
        {
            var warehouses = new SelectList(_db.Warehouses, "Id", "Name");
            var products = new SelectList(_db.Products, "Id", "Name");
            ViewBag.Warehouses = warehouses;
            ViewBag.Products = products;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Transaction transaction, string[] productCount)
        {
            if (transaction.WarehouseFromId != transaction.WarehouseInId)
            {
                var _transactions = CreateTransactions(transaction, productCount);
                _db.Transactions.AddRange(_transactions);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Create");
        }

        public IActionResult Report()
        {
            ViewBag.Warehouses = new SelectList(_db.Warehouses, "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Report(int? id, DateTime date)
        {
            ViewBag.Warehouses = new SelectList(_db.Warehouses, "Id", "Name");

            if (id == null && date == null)
            {
                return RedirectToAction("Report");
            }

            return View(await GetBalance(id, date));
        }

        [HttpGet]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int? id)
        {
            if (id != null)
            {
                var transactions = await _db.Transactions.FirstOrDefaultAsync(t => t.Id == id);
                if (transactions != null)
                    return View(transactions);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                var transactions = new Transaction { Id = id.Value };
                _db.Entry(transactions).State = EntityState.Deleted;
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return NotFound();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private List<Transaction> CreateTransactions(Transaction transaction, string[] productCount)
        {
            var _listTransactions = new List<Transaction>();
            var _from = transaction.WarehouseFromId;
            var _to = transaction.WarehouseInId;
            var _date = DateTime.Now;

            for (int i = 0; i < productCount.Length; i++)
            {
                if (productCount[i] != null && Convert.ToInt32(productCount[i]) > 0)
                {
                    var _transactionPlus = new Transaction
                    {
                        WarehouseFromId = _from,
                        WarehouseInId = _to,
                        ProductId = i + 1,
                        Count = Convert.ToInt32(productCount[i]),
                        DateTime = _date
                    };
                    _listTransactions.Add(_transactionPlus);
                    var _transactionMinus = new Transaction
                    {
                        WarehouseFromId = _to,
                        WarehouseInId = _from,
                        ProductId = i + 1,
                        Count = -Convert.ToInt32(productCount[i]),
                        DateTime = _date
                    };
                    _listTransactions.Add(_transactionMinus);
                }
            }
            return _listTransactions;
        }
        private Task<List<Transaction>> GetBalance(int? id, DateTime date)
        {
            var _balance = _db.Transactions
                    .Where(x => x.WarehouseInId == id && x.DateTime.Date <= date.Date)
                    .GroupBy(x => x.ProductId)
                    .Select(x => new Transaction
                    {
                        Id = x.Key,
                        WarehouseFromId = x.First().WarehouseFromId,
                        WarehouseInId = x.First().WarehouseInId,
                        ProductId = x.First().ProductId,
                        Count = x.Sum(x => x.Count),
                        DateTime = x.First().DateTime,
                        WarehouseFrom = x.First().WarehouseFrom,
                        WarehouseIn = x.First().WarehouseIn,
                        Product = x.First().Product
                    })
                    .OrderBy(x => x.Id)
                    .ToListAsync();
            return _balance;
        }
    }
}
