using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit.Search;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StoreFrontApp.DATA.EF.Models;
using X.PagedList;//Paged List - Step 2

namespace StoreFrontApp.UI.MVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductsController : Controller
    {
        private readonly StoreFrontAppContext _context;

        public ProductsController(StoreFrontAppContext context)
        {
            _context = context;
        }

        // GET: Products
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var products = _context.Products.Where(p => !p.IsDiscontinued)
                .Include(p => p.Category)
                .Include(p => p.Season);
            return View(await products.ToListAsync());
        }

        #region Filter/Paging Steps
        //---- SEARCH ----//
        //1) Create form in the view (for the SEARCH portion, only need 1 textbox and a submit button - <select> will be added later)
        //2) Update controller Action ([A] add param, [B]add search filter logic)

        //---- DDL ----//
        //1) Create ViewData[] object in Controller action (this sends DDL list to the View)
        //2) Add <select> inside of <form>
        //3) Update Controller Action ([A] add param, [B] add category filter logic)

        //---- PAGED LIST ----//
        //1) Install package for X.PagedList.Mvc.Core
        //      - Open Package Manger Console -> select the UI project -> install-package x.pagedlist.mvc.core
        //2) Add using statements and update model declaration in the View
        //3) Add param to Controller Action
        //4) Add page size variable in Action
        //5) Update return statement in Controller Action
        //6) Add Counter in View

        // 7) Create a new CSS file in wwwroot/css named 'PagedList.css'
        //      - NOTE: may need to go to the package's NuGet page and copy the CSS directly OR copy from an existing project :)
        //      - X.PagedList CSS file link (go here to copy the code): https://github.com/dncuug/X.PagedList/blob/master/examples/X.PagedList.Mvc.Example.Core/wwwroot/css/PagedList.css
        //8) Add a <link> to the _Layout referencing 'PagedList.css'
        #endregion



        //Created a separate action that returns the same results as Index, but in the View
        //we will use a tiled layout instead of a table

        //Search - Step 2 ---------------------------- 2.A ------------- DDL - Step 3 ------- Paged List - 3

        // GET: TiledProducts
        [AllowAnonymous]
        public async Task<IActionResult> TiledProducts(string searchTerm, int categoryId = 0, int page = 1)
        {
            // Paged List - Step 4
            int pageSize = 6;// our tilled view displayes rows of 3 products, so this will indicate how many total products to show on a page




            var products = _context.Products.Where(p => !p.IsDiscontinued)
                .Include(p => p.Category)
                .Include(p => p.Season)
                .Include(p => p.OrderProducts).ToList() ;

            // DDL - Step 1
            // Note: we copied this line from the existing functionality in Products.Create()
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName");

            // DDL - Step 3
            // Add Logic to filter the results by categoryId
            #region Optional Category Filter
            if (categoryId != 0)
            {
                products = products.Where(p => p.CategoryId == categoryId).ToList();
                // Recreate the dropdown list so the current category is still selected
                ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", categoryId);

            }
            #endregion

            //Search - Step 2.B
            #region Optional Search Filter
            if (!String.IsNullOrEmpty(searchTerm))
            {
                products = products.Where(p =>
                                    p.ProductName.ToLower().Contains(searchTerm.ToLower())
                                    || p.ProductDescription.ToLower().Contains(searchTerm.ToLower())
                                    || p.Category.CategoryName.ToLower().Contains(searchTerm.ToLower())).ToList();

                ViewBag.SearchTerm = searchTerm;
                ViewBag.NbrResults = products.Count;
            }
            else
            {
                ViewBag.SearchTerm = null;
                ViewBag.NbrResults = null;
            }
            #endregion

            // Paged List - Step 5
            return View(products.ToPagedList(page, pageSize));
        }

        // GET: Products/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Season)
                .Include(p => p.InventoryToProducts)//
                .ThenInclude(p => p.Inventory)//
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);

        }public async Task<IActionResult> Customize(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Season)
                .Include(p => p.InventoryToProducts)//
                .ThenInclude(p => p.Inventory)//
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName");
            ViewData["SeasonId"] = new SelectList(_context.Seasons, "SeasonId", "SeasonDescription");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "User")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,ProductName,ProductPrice,ProductDescription,IsInStock,IsDiscontinued,CategoryId,SeasonId,ProductImage")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", product.CategoryId);
            ViewData["SeasonId"] = new SelectList(_context.Seasons, "SeasonId", "SeasonDescription", product.SeasonId);
            return View(product);
        }

        // GET: Products/Edit/5
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Edit(int? id)
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", product.CategoryId);
            ViewData["SeasonId"] = new SelectList(_context.Seasons, "SeasonId", "SeasonDescription", product.SeasonId);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "User")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,ProductName,ProductPrice,ProductDescription,IsInStock,IsDiscontinued,CategoryId,SeasonId,ProductImage")] Product product)
        {
            if (id != product.ProductId)
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
                    if (!ProductExists(product.ProductId))
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", product.CategoryId);
            ViewData["SeasonId"] = new SelectList(_context.Seasons, "SeasonId", "SeasonDescription", product.SeasonId);
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Season)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'StoreFrontAppContext.Products'  is null.");
            }
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }
    }
}
