using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication_ColmanFactory1.Models;

namespace WebApplication_ColmanFactory1.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Products
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            try
            {
                var applicationDbContext = _context.Products.Include(p => p.Category);
                return View(await applicationDbContext.ToListAsync());

            }
            catch { return RedirectToAction("PageNotFound", "Home"); }
        }


        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return RedirectToAction("PageNotFound", "Home");
                }

                var product = await _context.Products
                    .Include(p => p.Category)
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (product == null)
                {
                    return RedirectToAction("PageNotFound", "Home");
                }

                return View(product);
            }
            catch { return RedirectToAction("PageNotFound", "Home");  }
        }

        // GET: Products/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Price,CategoryId,imagePath,IsOnSale")] Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(product);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id", product.CategoryId);
                return View(product);
            }
            catch { return RedirectToAction("PageNotFound", "Home"); }
        }

        // GET: Products/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return RedirectToAction("PageNotFound", "Home");
                }

                var product = await _context.Products.FindAsync(id);
                if (product == null)
                {
                    return  RedirectToAction("PageNotFound", "Home");
                }
                ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id", product.CategoryId);
                return View(product);
            }
            catch { return RedirectToAction("PageNotFound", "Home");}
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,CategoryId,imagePath,IsOnSale")] Product product)
        {
            try
            {
                if (id != product.Id)
                {
                    return RedirectToAction("PageNotFound", "Home");
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
                            return RedirectToAction("PageNotFound", "Home");
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return RedirectToAction(nameof(Index));
                }
                ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id", product.CategoryId);
                return View(product);
            }
            catch { return RedirectToAction("PageNotFound", "Home"); }
        }

        // GET: Products/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return RedirectToAction("PageNotFound", "Home");
                }

                var product = await _context.Products
                    .Include(p => p.Category)
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (product == null)
                {
                    return RedirectToAction("PageNotFound", "Home");
                }

                return View(product);
            }
            catch { return RedirectToAction("PageNotFound", "Home"); }
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var product = await _context.Products.FindAsync(id);
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch { return RedirectToAction("PageNotFound", "Home"); }
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }

        public async Task<IActionResult> Search(string productName, string category, string price)
        {
            try
            {
                var applicationDbContext = _context.Products.Include(a => a.Category).Where(a => a.Name.Contains(productName) && a.Category.Name.Equals(category) && a.Price <= Int32.Parse(price));
                return View("searchList", await applicationDbContext.ToListAsync());
            }
            catch { return RedirectToAction("PageNotFound", "Home"); }
        }

        public async Task<IActionResult> SearchByName(string productName)
        {
            try
            {
                var applicationDbContext = _context.Products.Include(a => a.Category).Where(a => a.Name.Contains(productName));
                return View("searchList", await applicationDbContext.ToListAsync());
            }
            catch { return RedirectToAction("PageNotFound", "Home"); }
        }
    }

}
