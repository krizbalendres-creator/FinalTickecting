using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BoholBusTicketing.Data.Data;
using BoholBusTicketing.Core.Models;
using BoholBusTicketing.Core.Interfaces;

namespace BoholBusTicketing.Data.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private readonly ApplicationDbContext _db;
        public TicketRepository(ApplicationDbContext db) { _db = db; }

        public async Task<Ticket> AddAsync(Ticket ticket)
        {
            _db.Tickets.Add(ticket);
            await _db.SaveChangesAsync();
            // ensure navigation props loaded if needed
            await _db.Entry(ticket).Reference(t => t.FromBarangay).LoadAsync();
            await _db.Entry(ticket).Reference(t => t.ToBarangay).LoadAsync();
            if (ticket.ConductorId.HasValue)
            {
                await _db.Entry(ticket).Reference(t => t.Conductor).LoadAsync();
            }
            return ticket;
        }

        public async Task<Ticket?> GetByIdAsync(int id)
        {
            return await _db.Tickets
                .AsNoTracking()
                .Include(t => t.FromBarangay)
                    .ThenInclude(b => b.Municipality)
                .Include(t => t.ToBarangay)
                    .ThenInclude(b => b.Municipality)
                .Include(t => t.Conductor)
                .FirstOrDefaultAsync(t => t.Id == id);
        }
    }
}
