using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication_ColmanFactory1.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    :    base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categorys { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Cart> Carts { get; set; }

        public DbSet<CartItem> CartItems { get; set; }
    }
}
