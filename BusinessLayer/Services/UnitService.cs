using BusinessLayer.Iservices;
using DataAccessLayer;
using DataAccessLayer.DTOs.Unit;
using DataAccessLayer.Models;
using DataAccessLayer.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class UnitService: IUnitService
    {
        public readonly IUnitOfWork _unitOfWork;
        public UnitService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
           
        public List<AllUnitsDto> getAll() {

            var units = _unitOfWork.Select<Unit>(x => x.isDeleted != true,s=>s.UnitCategory).Select(u => new AllUnitsDto
            {
                Id=u.Id,
                Image= u.Image,
                Location= u.Location,
                Price= u.Price,
                Title= u.Title,
                UnitCategoryName=u.UnitCategory.Name,
                
            }).ToList();
            return units;
         }
        public UnitDto? GetUnit(int id)
        {
           var unit= _unitOfWork.Select<Unit>(x=>x.Id==id,u=>u.UnitCategory).Select(s=>new UnitDto { 
            Id=id,
            Area= s.Area,
            Bathrooms= s.Bathrooms,
            Bedrooms= s.Bedrooms,
            DateBuilt= s.DateBuilt,
            Description= s.Description,
            Garage= s.Garage,
            Garden= s.Garden,
            Image= s.Image,
            IsRent = s.IsRent,
            Latitude= s.Latitude,
            Longitude= s.Longitude,
            Level= s.Level,
            Location= s.Location,
            Price= s.Price,
            Title= s.Title,
            UnitCategoryName=s.UnitCategory.Name,
            
            }).FirstOrDefault();
            return unit;
        }

        public async Task createUnit(Unit unit) {
            try {
                await _unitOfWork.UnitRepository.CreateAsync(unit);
                _unitOfWork.SaveAsync();
            }catch  (Exception ex)
            {
                throw ex;
            }
            
        }

        public async Task AddToFavorate(string UserId, int UnitId) {
            var favorite=new Favorite() {
            UnitId=UnitId,
            UserId=UserId,
            };

            await _unitOfWork.Add(favorite);
            await _unitOfWork.SaveAsync();
        }
        public async Task RemoveFavorite(string UserId, int UnitId)
        {
            var favorite = _unitOfWork.Select<Favorite>(x=>x.UserId == UserId && x.UnitId==UnitId).FirstOrDefault();
            if (favorite!=null)
            {
                _unitOfWork.Delete<Favorite>(favorite);
                await _unitOfWork.SaveAsync();
            }
                
                
            await _unitOfWork.SaveAsync();
        }

        public List<AllUnitsDto> GetFavoritList(string UserId) { 
        
            var units = new List<AllUnitsDto>();
            var ListOfUnitsIds=_unitOfWork.Select<Favorite>(x=>x.UserId==UserId).Select(x=>x.UnitId).ToList();
             units = _unitOfWork.Select<Unit>(x => x.isDeleted != true && ListOfUnitsIds.Contains(x.Id), s => s.UnitCategory).Select(u => new AllUnitsDto
            {
                Id = u.Id,
                Image = u.Image,
                Location = u.Location,
                Price = u.Price,
                Title = u.Title,
                UnitCategoryName = u.UnitCategory.Name,

            }).ToList();
            return units;
        }

		public List<AllUnitsDto> FilerUnits(string title,string governate,int priceFrom,int priceTo)
		{

            return _unitOfWork.UnitRepository.FilerUnits(title, governate, priceFrom, priceTo);
		}
	}
}
