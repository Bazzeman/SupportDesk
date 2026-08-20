using Microsoft.EntityFrameworkCore;
using SupportDesk.Data;
using SupportDesk.Data.Dtos;
using SupportDesk.Data.Entities;

namespace SupportDesk.Services
{
    public class MessageService(ApplicationDbContext context)
    {
        public async Task<IEnumerable<MessageDto>> GetMessagesByTicketIdAsync(int ticketId) =>
            await context.Messages
                .Where(m => m.TicketId == ticketId)
                .Include(m => m.Author)
                .Select(m => new MessageDto
                {
                    Id = m.Id,
                    Content = m.Content,
                    AuthorId = m.AuthorId,
                    AuthorFullName = m.Author.FullName,
                    Status = m.Status,
                    PostDate = m.PostDate,
                    TicketId = m.TicketId
                })
                .ToListAsync();

        public async Task<MessageDto?> GetMessageByIdAsync(int id) =>
            await context.Messages
                .Where(m => m.Id == id)
                .Include(m => m.Author)
                .Select(m => new MessageDto
                {
                    Id = m.Id,
                    Content = m.Content,
                    AuthorId = m.AuthorId,
                    AuthorFullName = m.Author.FullName,
                    Status = m.Status,
                    PostDate = m.PostDate,
                    TicketId = m.TicketId
                })
                .FirstAsync();

        public async Task<bool> CreateMessageAsync(MessageDto dto)
        {
            var entity = new Message
            {
                Content = dto.Content,
                Status = MessageStatus.Posted,
                AuthorId = dto.AuthorId,
                TicketId = dto.TicketId,
            };

            context.Messages.Add(entity);

            int changes = await context.SaveChangesAsync();

            return changes > 0;
        }

        public async Task<bool> UpdateMessageAsync(MessageDto dto)
        {
            var entity = await context.Messages.FindAsync(dto.Id);

            if (entity == null)
            {
                return false;
            }

            entity.Content = dto.Content;
            entity.Status = MessageStatus.Edited;
            entity.PostDate = DateTime.Now;

            int changes = await context.SaveChangesAsync();

            return changes > 0;
        }

        public async Task<bool> DeleteMessageAsync(int id)
        {
            var entity = await context.Messages.FindAsync(id);

            if (entity == null)
            {
                return false;
            }

            entity.Status = MessageStatus.Deleted;
            entity.PostDate = DateTime.Now;

            int changes = await context.SaveChangesAsync();

            return changes > 0;
        }
    }
}
