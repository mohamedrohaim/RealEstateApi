




using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Iservices
{
    public interface IUnitCategoryService
    {
        Task<IEnumerable<UnitCategory>> GetAllUnitCategoriesAsync();
        Task<UnitCategory?> GetUnitCategoryByIdAsync(int id);
        Task<UnitCategory?> GetUnitCategoryByNameAsync(string name);
        Task<object> CreateUnitCategoryAsync(string CategoryName);
        Task UpdateUnitCategoryAsync(int id, string categoryName);
        Task<object> DeleteUnitCategoryAsync(UnitCategory category);
        Task<bool> IsUnitCategoryExistsAsync(int id);
    }
}
