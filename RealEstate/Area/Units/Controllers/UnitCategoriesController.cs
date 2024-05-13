using BusinessLayer.Iservices;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RealEstate.Area.Units.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnitCategoriesController : ControllerBase
    {
        private readonly IUnitCategoryService _unitCategoryService;

        public UnitCategoriesController(IUnitCategoryService unitCategoryService)
        {
            _unitCategoryService = unitCategoryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UnitCategory>>> GetUnitCategories()
        {
            var unitCategories = await _unitCategoryService.GetAllUnitCategoriesAsync();
            return Ok(unitCategories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UnitCategory>> GetUnitCategory(int id)
        {
            var unitCategory = await _unitCategoryService.GetUnitCategoryByIdAsync(id);

            if (unitCategory == null)
            {
                return NotFound();
            }

            return Ok(unitCategory);
        }

        [HttpPost]
        public async Task<ActionResult<UnitCategory>> CreateUnitCategory(string categoryName)
        {
            var result = await _unitCategoryService.CreateUnitCategoryAsync(categoryName);

            if (result is UnitCategory)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUnitCategory(int id, string categoryName)
        {
            await _unitCategoryService.UpdateUnitCategoryAsync(id, categoryName);
            return Ok("Unit Category Updated Successfully");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUnitCategory(int id)
        {
            var unitCategory = await _unitCategoryService.GetUnitCategoryByIdAsync(id);
            if (unitCategory == null)
            {
                return NotFound();
            }

            var result = await _unitCategoryService.DeleteUnitCategoryAsync(unitCategory);

            if (result is bool && (bool)result)
            {
                return Ok("Unit Category Deleted Successfully");
            }
            else
            {
                return BadRequest(result);
            }
        }




    }
}
