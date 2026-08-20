namespace SupportDesk.Data.ViewModels
{
    public sealed class TicketFormViewModel
    {
        public required int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }

    public sealed record TicketViewModel(
        string Title,
        string Description,
        DateTime CreationDate,
        IEnumerable<MessageViewModel> Messages,
        CreateMessageViewModel NewMessage);

    public sealed record TicketSummaryViewModel(
        int Id,
        string Title,
        string Description,
        DateTime CreationDate);
}
