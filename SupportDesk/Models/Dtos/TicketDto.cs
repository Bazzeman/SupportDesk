namespace SupportDesk.Models.Dtos
{
    public class TicketDto
    {
        public int Id { get; set; }

        public required string Title { get; set; }

        public required string Description { get; set; }

        public DateTime CreationDate { get; set; }
    }
}
