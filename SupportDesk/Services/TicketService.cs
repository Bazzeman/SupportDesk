using Microsoft.EntityFrameworkCore;
using SupportDesk.Data;
using SupportDesk.Data.Dtos;
using SupportDesk.Data.Entities;

namespace SupportDesk.Services
{
    public class TicketService(ApplicationDbContext context)
    {
        public async Task<IEnumerable<TicketDto>> GetTicketsAsync() => 
            await context.Tickets
                .Select(t => new TicketDto
                {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    CreationDate = t.CreationDate
                })
                .ToListAsync();

        public async Task<TicketDto?> GetTicketByIdAsync(int id) =>
            await context.Tickets
                .Where(t => t.Id == id)
                .Select(t => new TicketDto
                {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    CreationDate = t.CreationDate
                })
                .FirstAsync();

        public async Task<bool> CreateTicketAsync(TicketDto dto)
        {
            var entity = new Ticket
            {
                Title = dto.Title,
                Description = dto.Description,
            };

            context.Tickets.Add(entity);

            int changes = await context.SaveChangesAsync();

            return changes > 0;
        }

        public async Task<bool> DeleteTicketAsync(int id)
        {
            var ticket = await context.Tickets.FindAsync(id);

            if (ticket == null)
            {
                return false;
            }

            context.Tickets.Remove(ticket);

            int changes = await context.SaveChangesAsync();

            return changes > 0;
        }
    }
}
