using Microsoft.AspNetCore.Mvc;
using BoholBusTicketing.Core.Interfaces;

namespace BoholBusTicketing.Web.Controllers
{
    public class LocationController : Controller
    {
        private readonly IMunicipalityRepository _municipalityRepository;
        private readonly IBarangayRepository _barangayRepository;

        public LocationController(IMunicipalityRepository municipalityRepository, IBarangayRepository barangayRepository)
        {
            _municipalityRepository = municipalityRepository;
            _barangayRepository = barangayRepository;
        }

        public async Task<IActionResult> Index()
        {
            var municipalities = await _municipalityRepository.GetAllAsync();
            return View(municipalities);
        }

        public async Task<IActionResult> GetBarangays(int municipalityId)
        {
            var barangays = await _barangayRepository.GetByMunicipalityIdAsync(municipalityId);
            return Json(barangays);
        }
    }
}