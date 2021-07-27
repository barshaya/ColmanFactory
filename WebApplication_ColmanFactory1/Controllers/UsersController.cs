﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication_ColmanFactory1.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;


namespace WebApplication_ColmanFactory1.Controllers
{
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            //HttpContext.Session.Clear();
            try
            {

                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

                return RedirectToAction(nameof(Index), "Home");
            }
            catch { return RedirectToAction("PageNotFound", "Home"); }
        }

        private async void Signin(User account)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, account.Username),
                new Claim(ClaimTypes.Role, account.Type.ToString()),
            };
            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                //ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10)
            };


            await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);
        }

        // GET: Users/Register
        public IActionResult Register()
        {
            return View();
        }

        // POST: Users/Register
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("Username,Password,Email,PhoneNumber")] User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var q = _context.Users.FirstOrDefault(u => u.Username == user.Username);
                    if (q == null)
                    {
                        _context.Add(user);
                        await _context.SaveChangesAsync();
                        var u = _context.Users.FirstOrDefault(u => u.Username == user.Username);
                        Signin(u);
                        return RedirectToAction(nameof(Index), "Home");
                    }
                    else
                    {
                        ViewData["Error"] = "Unable to comply ; cannot register this user.";
                    }
                }
                return View(user);
            }
            catch { return RedirectToAction("PageNotFound", "Home"); }
        }

        // GET: Users/Login
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login([Bind("Id,Username,Password")] User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var q = _context.Users.FirstOrDefault(u => u.Username == user.Username && u.Password == user.Password);
                    if (q != null)
                    {
                        Signin(q);
                        return RedirectToAction(nameof(Index), "Home");
                    }
                    else
                    {
                        ViewData["Error"] = "Unable to comply ; cannot register this user.";
                    }
                }
                return View(user);
            }
            catch { return RedirectToAction("PageNotFound", "Home"); }
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Search(string Username, string role)
        {
            try
            {
                int type = 0;
                if (role.Equals("Admin"))
                    type = 1;

                return View(await _context.Users.Where(u => u.Username.Contains(Username) && (int)u.Type == type).ToListAsync());

            }
            catch { return RedirectToAction("PageNotFound", "Home"); }
        }



        // GET: Users
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            try
            {
                return View(await _context.Users.ToListAsync());
            }
            catch { return RedirectToAction("PageNotFound", "Home"); }
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return RedirectToAction("PageNotFound", "Home"); ;
                }


                var user = await _context.Users
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (user == null)
                {
                    return RedirectToAction("PageNotFound", "Home"); ;
                }

                return View(user);
            }
            catch { return RedirectToAction("PageNotFound", "Home"); }
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserID,Username,Password,Email,PhoneNumber,Type")] User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(user);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(user);
            }
            catch { return RedirectToAction("PageNotFound", "Home"); }
        }

        // GET: Users/Edit/5
        [Authorize(Roles = "Admin")]
        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return RedirectToAction("PageNotFound", "Home");
                }

                var user = await _context.Users.FindAsync(id);
                if (user == null)
                {
                    return RedirectToAction("PageNotFound", "Home");
                }
                return View(user);
            }
            catch { return RedirectToAction("PageNotFound", "Home"); }
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Username,Password,Email,PhoneNumber,Type")] User user)
        {
            try
            {
                if (id != user.Id)
                {
                    return RedirectToAction("PageNotFound", "Home");
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        user.Cart = _context.Carts.FirstOrDefault(x => x.Id == user.Id);
                        _context.Update(user);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!UserExists(user.Id))
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
                return View(user);
            }
            catch { return RedirectToAction("PageNotFound", "Home"); }
        }

        // GET: Users/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return RedirectToAction("PageNotFound", "Home");
                }

                var user = await _context.Users
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (user == null)
                {
                    return RedirectToAction("PageNotFound", "Home");
                }

                return View(user);
            }
            catch { return RedirectToAction("PageNotFound", "Home"); }
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var user = await _context.Users.FindAsync(id);
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch { return RedirectToAction("PageNotFound", "Home"); }
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}