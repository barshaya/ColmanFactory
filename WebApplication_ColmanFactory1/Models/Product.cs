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
        // Product ID
        [Key]
        [Required(ErrorMessage = "You must enter a Product ID")]
        public int ProductID { get; set; }

        // Product Name
        [DisplayName("Name")]
        [Required(ErrorMessage = "You must enter a name")]
        [RegularExpression("^[A-Z][a-zA-Z ]*$", ErrorMessage = "The body must contains only letters and start with one uppercase letter")]
        public string ProductName { get; set; }

        [DisplayName("Image")]
        public string ProductImagePath { get; set; }

        [DisplayName("Price")]
        [Required(ErrorMessage = "You must enter a name")]
        [Range(0, 500)]
        [DataType(DataType.Currency)]
        public double ProductPrice { get; set; }

        [Required]
        public string ProductColor { get; set; }

        //Required]
        //public List<Int32> ProductSizeList { get; set; }

        [DisplayName("Description")]
        public string ProductDescription { get; set; }

        //one -> many : Category -> Product
        public int CategoryID { get; set; }
        public Category Category { get; set; }


        //many <-> many : Cart <-> Product
        public List<Cart> ProductCartList { get; set; }
    }
}
