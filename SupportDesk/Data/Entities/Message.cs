namespace SupportDesk.Data.Entities
{
    public class Message
    {
        public int Id { get; set; }

        public required string Content { get; set; }

        public DateTime PostDate { get; set; } = DateTime.Now;

        public required MessageStatus Status { get; set; }

        public required string AuthorId { get; set; }

        public ApplicationUser? Author { get; set; }

        public required int TicketId { get; set; }

        public Ticket? Ticket { get; set; }
    }
}
