using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StoreFrontApp.DATA.EF.Models;

namespace StoreFrontApp.UI.MVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class InventoryToProductController : Controller
    {
        private readonly StoreFrontAppContext _context;

        public InventoryToProductController(StoreFrontAppContext context)
        {
            _context = context;
        }

        // GET: InventoryToProduct
        
        public async Task<IActionResult> Index()
        {
            var storeFrontAppContext = _context.InventoryToProducts.Include(i => i.Inventory).Include(i => i.Product);
            return View(await storeFrontAppContext.ToListAsync());
        }

        // GET: InventoryToProduct/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.InventoryToProducts == null)
            {
                return NotFound();
            }

            var inventoryToProduct = await _context.InventoryToProducts
                .Include(i => i.Inventory)
                .Include(i => i.Product)
                .FirstOrDefaultAsync(m => m.InvToProdId == id);
            if (inventoryToProduct == null)
            {
                return NotFound();
            }

            return View(inventoryToProduct);
        }

        // GET: InventoryToProduct/Create
        public IActionResult Create()
        {
            ViewData["InventoryId"] = new SelectList(_context.Inventories, "InventoryId", "InventoryName");
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductName");
            return View();
        }

        // POST: InventoryToProduct/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InvToProdId,InventoryId,ProductId")] InventoryToProduct inventoryToProduct)
        {
            if (ModelState.IsValid)
            {
                _context.Add(inventoryToProduct);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["InventoryId"] = new SelectList(_context.Inventories, "InventoryId", "InventoryName", inventoryToProduct.InventoryId);
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductName", inventoryToProduct.ProductId);
            return View(inventoryToProduct);
        }

        // GET: InventoryToProduct/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.InventoryToProducts == null)
            {
                return NotFound();
            }

            var inventoryToProduct = await _context.InventoryToProducts.FindAsync(id);
            if (inventoryToProduct == null)
            {
                return NotFound();
            }
            ViewData["InventoryId"] = new SelectList(_context.Inventories, "InventoryId", "InventoryName", inventoryToProduct.InventoryId);
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductName", inventoryToProduct.ProductId);
            return View(inventoryToProduct);
        }

        // POST: InventoryToProduct/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("InvToProdId,InventoryId,ProductId")] InventoryToProduct inventoryToProduct)
        {
            if (id != inventoryToProduct.InvToProdId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(inventoryToProduct);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InventoryToProductExists(inventoryToProduct.InvToProdId))
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
            ViewData["InventoryId"] = new SelectList(_context.Inventories, "InventoryId", "InventoryName", inventoryToProduct.InventoryId);
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductName", inventoryToProduct.ProductId);
            return View(inventoryToProduct);
        }

        // GET: InventoryToProduct/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.InventoryToProducts == null)
            {
                return NotFound();
            }

            var inventoryToProduct = await _context.InventoryToProducts
                .Include(i => i.Inventory)
                .Include(i => i.Product)
                .FirstOrDefaultAsync(m => m.InvToProdId == id);
            if (inventoryToProduct == null)
            {
                return NotFound();
            }

            return View(inventoryToProduct);
        }

        // POST: InventoryToProduct/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.InventoryToProducts == null)
            {
                return Problem("Entity set 'StoreFrontAppContext.InventoryToProducts'  is null.");
            }
            var inventoryToProduct = await _context.InventoryToProducts.FindAsync(id);
            if (inventoryToProduct != null)
            {
                _context.InventoryToProducts.Remove(inventoryToProduct);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InventoryToProductExists(int id)
        {
          return _context.InventoryToProducts.Any(e => e.InvToProdId == id);
        }
    }
}
