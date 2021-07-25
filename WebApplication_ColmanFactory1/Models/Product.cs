using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication_ColmanFactory1.Models
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }

        public string Name { get; set; }

        public string ImagePath { get; set; }

        public double Price { get; set; }

        public bool IsOnSale { get; set; }

        public int CategoryID { get; set; }

        public Category Category { get; set; }
    }
}
