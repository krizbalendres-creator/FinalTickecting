using Microsoft.AspNetCore.Mvc;
using BoholBusTicketing.Core.Interfaces;

namespace BoholBusTicketing.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMunicipalityRepository _municipalityRepository;
        private readonly ITicketService _ticketService;

        public HomeController(ILogger<HomeController> logger, IMunicipalityRepository municipalityRepository, ITicketService ticketService)
        {
            _logger = logger;
            _municipalityRepository = municipalityRepository;
            _ticketService = ticketService;
        }

        public async Task<IActionResult> Index()
        {
            var municipalities = await _municipalityRepository.GetAllAsync();
            ViewData["MunicipalityCount"] = municipalities.Count();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }
    }
}