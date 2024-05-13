using DataAccessLayer.Data;
using DataAccessLayer.DTOs.Unit;
using DataAccessLayer.IRepository;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository
{
    public class UnitRepository : Repository<Unit>,IUnitRepository
    {
        private readonly AppDbContext _context;
        public UnitRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
		public List<AllUnitsDto> FilerUnits(string title, string governate, int priceFrom, int priceTo)
		{
			var units = _context.units.Where(x =>
			(
			x.Price >= priceFrom && x.Price <= priceTo)
			).Include(x=> x.UnitCategory).ToList();
			if (units.Count > 0 && !string.IsNullOrEmpty(title))
			{
				units = units.Where(x => x.Title.ToUpperInvariant().Contains(title.ToUpper())).ToList();
			}
			if (units.Count > 0 && !string.IsNullOrEmpty(governate))
			{
				units = units.Where(x => x.Location.ToUpperInvariant().Contains(governate.ToUpper())).ToList();
			}
			return units.Select(x => new AllUnitsDto
			{
				UnitCategoryName = x.UnitCategory.Name,
				Location = x.Location,
				Price = x.Price,
				Title = x.Title,
				Image = x.Image,
				Id = x.Id,
			}).ToList();
		}
	}
}
