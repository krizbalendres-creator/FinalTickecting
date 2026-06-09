using System.Collections.Generic;
using System.Threading.Tasks;
using BoholBusTicketing.Core.Models;

namespace BoholBusTicketing.Core.Interfaces
{
    public interface IMunicipalityRepository
    {
        Task<IEnumerable<Municipality>> GetAllAsync();
        Task<Municipality?> GetByIdAsync(int id);
    }
}
