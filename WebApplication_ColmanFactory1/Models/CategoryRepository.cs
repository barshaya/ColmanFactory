using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication_ColmanFactory1.Models
{
    public class CategoryRepository : ICategoryRepository
    {
        public List<Category> GetAllCategories => new List<Category>
        {
            new Category{ CategoryId=1, CategoryName="Women" },
            new Category{ CategoryId=1, CategoryName="Men" },
            new Category{ CategoryId=1, CategoryName="Boys" },
            new Category{ CategoryId=1, CategoryName="Girls" }
        };
    }
}
