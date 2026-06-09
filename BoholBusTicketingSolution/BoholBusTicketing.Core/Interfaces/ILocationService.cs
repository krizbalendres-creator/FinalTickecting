using System.Collections.Generic;
using System.Threading.Tasks;
using BoholBusTicketing.Core.Models;

namespace BoholBusTicketing.Core.Interfaces
{
    public interface ILocationService
    {
        Task<IEnumerable<Municipality>> GetAllMunicipalitiesAsync();
        Task<IEnumerable<Barangay>> GetBarangaysByMunicipalityAsync(int municipalityId);
        Task<Barangay?> GetBarangayByIdAsync(int id);
    }
}
