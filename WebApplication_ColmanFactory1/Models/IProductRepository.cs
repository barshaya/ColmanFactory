using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication_ColmanFactory1.Models
{
    public interface IProductRepository
    {
        List<Product> GetAllProducts { get;  }
        List<Product> GetProductsOnSale { get; }
        Product GetProductById(int productId);
    }
}
