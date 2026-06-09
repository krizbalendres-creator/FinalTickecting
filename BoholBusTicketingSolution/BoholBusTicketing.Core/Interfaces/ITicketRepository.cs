using System.Threading.Tasks;
using BoholBusTicketing.Core.Models;

namespace BoholBusTicketing.Core.Interfaces
{
    public interface ITicketRepository
    {
        Task<Ticket> AddAsync(Ticket ticket);
        Task<Ticket?> GetByIdAsync(int id);
    }
}
