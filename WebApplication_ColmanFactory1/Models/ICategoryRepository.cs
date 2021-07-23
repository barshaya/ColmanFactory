using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication_ColmanFactory1.Models
{
    public interface ICategoryRepository
    {
        List<Category> GetAllCategories { get; }
    }
}
