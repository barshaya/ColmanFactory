using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication_ColmanFactory1.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //one -> many : Category -> Product
        public List<Product> Products { get; set; }
    }
}