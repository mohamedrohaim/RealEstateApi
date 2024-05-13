using DataAccessLayer.DTOs.Unit;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Iservices
{
    public interface IUnitService
    {
        List<AllUnitsDto> getAll();
        List<AllUnitsDto> FilerUnits(string title,string governate,int priceFrom,int priceTo);
        UnitDto? GetUnit(int id);
        Task createUnit(Unit unit);
        Task AddToFavorate(string userId, int unitId);
        Task RemoveFavorite(string userId, int unitId);
        List<AllUnitsDto> GetFavoritList(string UserId);
    }
}
