using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication_ColmanFactory1.Models
{
    public class Cart
    {
        [Key]
        public int CartId { get; set; }

        public string CartName { get; set; }

        // one <-> one : Cart <-> User
        public int UserID { get; set; }
        public User User { get; set; }

        //many <-> many : Cart <-> Product 

        public List<Product> Products { get; set; }

        [DataType(DataType.Currency)]
        public int TotalAmount { get; set; }


    }
}
