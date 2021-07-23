using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication_ColmanFactory1.Models
{
    public class ProductRepository : IProductRepository
    {
        private readonly ICategoryRepository _categoryRepository = new CategoryRepository();
        public List<Product> GetAllProducts => new List<Product>
        {
            new Product{ProductID=1, ProductName="one", ProductPrice=11, ProductDescription="one desc is here", Category= _categoryRepository.GetAllCategories.ToList()[0],
            ProductImagePath="https://images.unsplash.com/photo-1595950653106-6c9ebd614d3a?ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&ixlib=rb-1.2.1&auto=format&fit=crop&w=334&q=80",
            IsInStock=true, IsOnSale=false},

            new Product{ProductID=2, ProductName="two", ProductPrice=12, ProductDescription="two desc is here", Category= _categoryRepository.GetAllCategories.ToList()[1],
            ProductImagePath="https://images.unsplash.com/photo-1600269452121-4f2416e55c28?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=401&q=80",
            IsInStock=true, IsOnSale=false},

            new Product{ProductID=2, ProductName="three", ProductPrice=13, ProductDescription="three desc is here", Category= _categoryRepository.GetAllCategories.ToList()[2],
            ProductImagePath="https://images.unsplash.com/photo-1597045566677-8cf032ed6634?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=334&q=80",
            IsInStock=true, IsOnSale=false},
        };

        public List<Product> GetProductsOnSale => throw new NotImplementedException();

        public Product GetProductById(int productId)
        {
            return GetAllProducts.FirstOrDefault(p => p.ProductID == productId);
        }
    }
}
