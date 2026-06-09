using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BoholBusTicketing.Data.Data;
using BoholBusTicketing.Core.Models;
using BoholBusTicketing.Core.Interfaces;

namespace BoholBusTicketing.Data.Repositories
{
    public class BarangayRepository : IBarangayRepository
    {
        private readonly ApplicationDbContext _db;
        public BarangayRepository(ApplicationDbContext db) { _db = db; }

        public async Task<IEnumerable<Barangay>> GetByMunicipalityIdAsync(int municipalityId)
        {
            return await _db.Barangays
                .AsNoTracking()
                .Where(b => b.MunicipalityId == municipalityId)
                .OrderBy(b => b.Name)
                .ToListAsync();
        }

        public async Task<Barangay?> GetByIdAsync(int id)
        {
            return await _db.Barangays
                .Include(b => b.Municipality)
                .FirstOrDefaultAsync(b => b.Id == id);
        }
    }
}
