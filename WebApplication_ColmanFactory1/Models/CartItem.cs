using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication_ColmanFactory1.Models
{
    public class CartItem
    {
        [Key]
        public int CartItemId { get; set; }
        public string CartId { get; set; }
        public Product product { get; set; }
        public int Amount { get; set; }
    }
}
