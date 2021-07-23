using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication_ColmanFactory1.Models
{
    public class Category
    {
        public int CategoryId { get; set; }

        [System.ComponentModel.DisplayName("Category Name")]
        public string CategoryName { get; set; }

        //one -> many : Category -> Product
        public List<Product> Products { get; set; }
    }
}