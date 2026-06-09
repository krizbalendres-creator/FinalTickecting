using System.Threading.Tasks;
using BoholBusTicketing.Core.DTOs;

namespace BoholBusTicketing.Core.Interfaces;

    public interface ITicketService
    {
        decimal ComputeFare(double distance);
        Task<double> EstimateDistanceAsync(int fromBarangayId, int toBarangayId);
        Task<int> CreateTicketAsync(CreateTicketInputDto dto, int? conductorId = null);
        Task<TicketOutputDto?> GetTicketAsync(int id);
    }

