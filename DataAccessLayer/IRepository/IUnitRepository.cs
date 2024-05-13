using DataAccessLayer.DTOs.Unit;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.IRepository
{
    public interface IUnitRepository:IRepository<Unit>
    {
		List<AllUnitsDto> FilerUnits(string title, string governate, int priceFrom, int priceTo);

	}
}
