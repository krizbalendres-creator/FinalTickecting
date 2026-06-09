using System.Collections.Generic;
using System.Threading.Tasks;
using BoholBusTicketing.Core.Models;

namespace BoholBusTicketing.Core.Interfaces
{
    public interface IBarangayRepository
    {
        Task<IEnumerable<Barangay>> GetByMunicipalityIdAsync(int municipalityId);
        Task<Barangay?> GetByIdAsync(int id);
    }
}
