using System.Collections.Generic;
using System.Threading.Tasks;
using BoholBusTicketing.Core.Models;
using BoholBusTicketing.Core.Interfaces;


namespace BoholBusTicketing.Core.Services
{
    public class LocationService : ILocationService
    {
        private readonly IMunicipalityRepository _municipalityRepository;
        private readonly IBarangayRepository _barangayRepository;

        public LocationService(IMunicipalityRepository municipalityRepository, IBarangayRepository barangayRepository)
        {
            _municipalityRepository = municipalityRepository;
            _barangayRepository = barangayRepository;
        }

        public LocationService()
        {
        }

        public Task<IEnumerable<Municipality>> GetAllMunicipalitiesAsync() => _municipalityRepository.GetAllAsync();

        public Task<IEnumerable<Barangay>> GetBarangaysByMunicipalityAsync(int municipalityId) =>
            _barangayRepository.GetByMunicipalityIdAsync(municipalityId);

        public Task<Barangay?> GetBarangayByIdAsync(int id) => _barangayRepository.GetByIdAsync(id);
    }
}
