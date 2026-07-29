namespace SupportDesk.Models.ViewModels
{
    public class TicketViewModel
    {
        public int Id { get; set; }

        public required string Title { get; set; }

        public required string Description { get; set; }

        public DateTime CreationDate { get; set; }
    }
}
