using Microsoft.AspNetCore.Mvc;
using BoholBusTicketing.Core.DTOs;
using BoholBusTicketing.Core.Interfaces;

namespace BoholBusTicketing.Web.Controllers
{
    public class TicketController : Controller
    {
        private readonly ITicketService _ticketService;
        private readonly IBarangayRepository _barangayRepository;

        public TicketController(ITicketService ticketService, IBarangayRepository barangayRepository)
        {
            _ticketService = ticketService;
            _barangayRepository = barangayRepository;
        }

        public async Task<IActionResult> Index()
        {
            var barangays = await _barangayRepository.GetByMunicipalityIdAsync(1);
            ViewBag.Barangays = barangays.ToList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CalculateFare(int fromBarangayId, int toBarangayId)
        {
            try
            {
                if (fromBarangayId == toBarangayId)
                    return Json(new { success = false, message = "Origin and destination cannot be the same." });

                var distance = await _ticketService.EstimateDistanceAsync(fromBarangayId, toBarangayId);
                var fare = _ticketService.ComputeFare(distance);

                return Json(new { success = true, distance, fare });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> BookTicket(int fromBarangayId, int toBarangayId, double distance, decimal fare)
        {
            try
            {
                var dto = new CreateTicketInputDto
                {
                    FromBarangayId = fromBarangayId,
                    ToBarangayId = toBarangayId,
                    Distance = distance,
                    Fare = fare
                };

                var ticketId = await _ticketService.CreateTicketAsync(dto);
                return Json(new { success = true, ticketId, message = $"Ticket #{ticketId} booked successfully!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}