namespace SupportDesk.Data.Dtos
{
    public class MessageDto
    {
        public int Id { get; set; }

        public required string Content { get; set; }

        public required string AuthorId { get; set; }

        public required string AuthorFullName { get; set; }

        public MessageStatus Status { get; set; }

        public DateTime PostDate { get; set; }

        public required int TicketId { get; set; }
    }
}
