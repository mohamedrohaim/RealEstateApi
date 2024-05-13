using BusinessLayer.Iservices;
using DataAccessLayer.DTOs.Unit;
using DataAccessLayer.DTOs;
using DataAccessLayer.Models;
using DataAccessLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Helpers.ImageUploader;

namespace RealEstate.Area.Units.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnitController : ControllerBase
    {
        private readonly IUnitService _unitService;
        private readonly IImageUploader _imageUploader;
        private readonly IUnitOfWork _unitOfWork;



        public UnitController(IUnitService unitService, IImageUploader imageUploader, IUnitOfWork unitOfWork)
        {
            _unitService = unitService;
            _imageUploader = imageUploader;
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public IActionResult GetAllUnits() {
            var units = _unitService.getAll();
            return Ok(units);

        }
        [HttpPost]
        public async Task<IActionResult> CreateUnit([FromForm] CreateUnitDto createUnitDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (createUnitDto.Image == null)
                {
                    return BadRequest("Image is required");
                }

                // Upload image
                var imageUploadResult = _imageUploader.TryUploadImage(createUnitDto.Image);
                if (imageUploadResult is ErrorDTO)
                {
                    return BadRequest(((ErrorDTO)imageUploadResult).Message);
                }

                var imageDto = (ImageDTO)imageUploadResult;
                var baseUrl = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

                var unit = new Unit
                {
                    Title = createUnitDto.Title,
                    Description = createUnitDto.Description,
                    Bedrooms = createUnitDto.Bedrooms,
                    Bathrooms = createUnitDto.Bathrooms,
                    Area = createUnitDto.Area,
                    Level = createUnitDto.Level,
                    IsRent = createUnitDto.IsRent,
                    Garage = createUnitDto.Garage,
                    Garden = createUnitDto.Garden,
                    DateBuilt = createUnitDto.DateBuilt,
                    Longitude = createUnitDto.Longitude,
                    Latitude = createUnitDto.Latitude,
                    Location = createUnitDto.Location,
                    UnitCategoryId = createUnitDto.UnitCategoryId,
                    Price = createUnitDto.Price,
                    isDeleted = false,
                    //UserId=createUnitDto.UserId,
                    CreatedAt = DateTime.Now,
                    Image = baseUrl + "/" + imageDto.ImageUrl

                };

                await _unitService.createUnit(unit);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpGet("{id}")]
        public IActionResult GetUnitById(int id, string userId)
        {
            try
            {
                var unit = _unitService.GetUnit(id);
                if (unit == null)
                {
                    return NotFound();
                }
                var isFaorite = _unitOfWork.Select<Favorite>(x => x.UserId == userId && x.UnitId == id).Any();
                return Ok(new { unit, isFaorite });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpPost("add-favorite")]
        public async Task<IActionResult> AddToFavorate([FromQuery] string userId, [FromQuery] int unitId)
        {
            try
            {
                await _unitService.AddToFavorate(userId, unitId);
                return Ok("Added to favorites successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("remove-favorite")]
        public async Task<IActionResult> RemoveFavorite([FromQuery] string userId, [FromQuery] int unitId)
        {
            try
            {
                await _unitService.RemoveFavorite(userId, unitId);
                return Ok("Removed from favorites successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("favorite-list")]
        public IActionResult GetAllUnits(string userId)
        {
            var units = _unitService.GetFavoritList(userId);
            return Ok(units);

        }

        [HttpPost("FilterUnits")]
        public IActionResult FilterUnits([FromBody] FilterUnitsDto dto){
            if (dto.priceFrom == null) {
            dto.priceFrom = 0;
            }
            if (dto.priceTo == null)
            {
                dto.priceTo = int.MaxValue;
            }
            var units=_unitService.FilerUnits(dto.title, dto.governate, (int)dto.priceFrom,(int) dto.priceTo);
            return Ok(units);
        }



	}
}
