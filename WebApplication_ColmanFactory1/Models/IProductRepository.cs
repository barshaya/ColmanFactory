using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication_ColmanFactory1.Models
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAllProducts { get; }
        IEnumerable<Product> GetProductsOnSale { get; }
        Product GetProductById(int candyId);
    }
}
