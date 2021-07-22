using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApplication_ColmanFactory1.Models;

namespace WebApplication_ColmanFactory1.Data
{
    public class WebApplication_ColmanFactory1Context : DbContext
    {
        public WebApplication_ColmanFactory1Context (DbContextOptions<WebApplication_ColmanFactory1Context> options)
            : base(options)
        {
        }

        public DbSet<WebApplication_ColmanFactory1.Models.Cart> Cart { get; set; }

        public DbSet<WebApplication_ColmanFactory1.Models.Product> Product { get; set; }

        public DbSet<WebApplication_ColmanFactory1.Models.Category> Category { get; set; }

        public DbSet<WebApplication_ColmanFactory1.Models.User> User { get; set; }
    }
}
