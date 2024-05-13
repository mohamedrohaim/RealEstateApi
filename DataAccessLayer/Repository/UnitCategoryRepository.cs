using DataAccessLayer.Data;
using DataAccessLayer.IRepository;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository
{
    public  class UnitCategoryRepository:Repository<UnitCategory>,IUnitCategoryRepository
    {
        private readonly AppDbContext _context;
        public UnitCategoryRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
