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
    public class CategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Categories
       
        public async Task<IActionResult> Index()
        {
            try
            {
                return View(await _context.Categories.ToListAsync());
            }
            catch { return RedirectToAction("PageNotFound", "Home"); }
        }

      
        public async Task<IActionResult> Search(string query)
        {
            try
            {
                return Json(await _context.Categories.Where(c => c.Name.Contains(query)).ToListAsync());
            }
            catch { return RedirectToAction("PageNotFound", "Home"); }
        }


        // GET: Categories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return RedirectToAction("PageNotFound", "Home");
                }

                var category = await _context.Categories
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (category == null)
                {
                    return RedirectToAction("PageNotFound", "Home");
                }

                return View(category);
            }
            catch { return RedirectToAction("PageNotFound", "Home"); }
        }

        // GET: Categories/Create
   
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Category category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    System.Diagnostics.Debug.WriteLine("hello");


                    _context.Add(category);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(category);
            }
            catch { return RedirectToAction("PageNotFound", "Home"); }
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return RedirectToAction("PageNotFound", "Home");
                }

                var category = await _context.Categories.FindAsync(id);
                if (category == null)
                {
                    return RedirectToAction("PageNotFound", "Home");
                }
                return View(category);
            }
            catch { return RedirectToAction("PageNotFound", "Home"); }
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Category category)
        {
            try
            {
                if (id != category.Id)
                {
                    return RedirectToAction("PageNotFound", "Home");
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(category);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!CategoryExists(category.Id))
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
                return View(category);
            }
            catch { return RedirectToAction("PageNotFound", "Home"); }
        }

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return RedirectToAction("PageNotFound", "Home");
                }

                var category = await _context.Categories
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (category == null)
                {
                    return RedirectToAction("PageNotFound", "Home");
                }

                return View(category);
            }
            catch { return RedirectToAction("PageNotFound", "Home"); }
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var category = await _context.Categories.FindAsync(id);
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch { return RedirectToAction("PageNotFound", "Home"); }
        }

        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.Id == id);
        }

        public async Task<IActionResult> Women()
        {
            return View(await _context.Products.Where(p => p.CategoryId == 1).ToListAsync());
        }

        public async Task<IActionResult> Men()
        {
            return View(await _context.Products.Where(p => p.CategoryId == 2).ToListAsync());
        }

        public async Task<IActionResult> Boys()
        {
            return View(await _context.Products.Where(p => p.CategoryId == 3).ToListAsync());
        }

        public async Task<IActionResult> Girls()
        {
            return View(await _context.Products.Where(p => p.CategoryId == 4).ToListAsync());
        }
    }
}
