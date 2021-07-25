using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication_ColmanFactory1.Models
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _appDbContext;

        public ProductRepository(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public IEnumerable<Product> GetAllProducts
        {
            get
            {
                return _appDbContext.Products.Include(c => c.Category);
            }
        }

        public IEnumerable<Product> GetProductsOnSale
        {
            get
            {
                return _appDbContext.Products.Include(c => c.Category).Where(p => p.IsOnSale);
            }
        }

        public Product GetProductById(int productId)
        {
            return _appDbContext.Products.FirstOrDefault(c => c.ProductID == productId);
        }
    }
}
