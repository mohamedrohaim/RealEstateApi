using BusinessLayer.Iservices;
using DataAccessLayer;
using DataAccessLayer.DTOs;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{

    public class UnitCategoryService:IUnitCategoryService
    {
        public readonly IUnitOfWork _unitOfWork;
        public UnitCategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<object> CreateUnitCategoryAsync(string CategoryName)
        {
            var errorModel = new ErrorDTO();
            try
            {

                var existingUnitCategory = await GetUnitCategoryByNameAsync(CategoryName);

                if (existingUnitCategory != null)
                {
                    errorModel.Message = $"Category {CategoryName} is created before";
                    return errorModel;
                }

                var unitCategory = new UnitCategory()
                {
                    CreatedAt = DateTime.Now,
                    Name = CategoryName,
                    isDeleted=false,
                    
                };

                await _unitOfWork.UnitCategoryRepository.CreateAsync(unitCategory);
                await _unitOfWork.SaveAsync();
                return unitCategory;

            }
            catch (Exception ex)
            {
                errorModel.Message = ex.Message;
                return errorModel;

            }
        }

        public async Task<object> DeleteUnitCategoryAsync(UnitCategory category)
        {
            try
            {
                category.isDeleted = true;
                _unitOfWork.UnitCategoryRepository.Update(category);
                await _unitOfWork.SaveAsync();
                return true;

            }
            catch (Exception ex)
            {
                var error = new ErrorDTO()
                {
                    Message = ex.Message,
                };
                return error;
            }
        }

        public async Task<IEnumerable<UnitCategory>> GetAllUnitCategoriesAsync()
        {
            return await _unitOfWork.UnitCategoryRepository.GetAllAsync();
        }

        public async Task<UnitCategory?> GetUnitCategoryByIdAsync(int id)
        {
            var unitCategory = await _unitOfWork.UnitCategoryRepository.GetAsync(c => c.Id == id);

            return unitCategory;
        }

        public async Task<UnitCategory?> GetUnitCategoryByNameAsync(string name)
        {
            var unitCategory = await _unitOfWork.UnitCategoryRepository.GetAsync(c => c.Name == name);

            return unitCategory;
        }

        public async Task<bool> IsUnitCategoryExistsAsync(int id)
        {
            var category = await _unitOfWork.UnitCategoryRepository.GetAsync(c => c.Id == id);
            return category != null;
        }

        public async Task UpdateUnitCategoryAsync(int id, string categoryName)
        {
            var unitCategory =await GetUnitCategoryByIdAsync(id);
            if (unitCategory!=null)
            {
                unitCategory.Name = categoryName;
                unitCategory.UpdatedAt = DateTime.Now;
                await _unitOfWork.SaveAsync();
            }
        }
    }
}
