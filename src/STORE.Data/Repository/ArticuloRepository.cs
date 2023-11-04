using STORE.Data.Context;
using STORE.Entities.Interface;
using STORE.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STORE.Data.Repository
{
    public class ArticuloRepository : GenericRepository<Articulo>, IArticuloRepository
    {
        private readonly ApplicationDbContext _context;
        public ArticuloRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
