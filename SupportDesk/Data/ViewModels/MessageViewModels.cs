namespace SupportDesk.Data.ViewModels
{
    public sealed class CreateMessageViewModel
    {
        public string? Content { get; set; }
        public int TicketId { get; set; }
    }

    public sealed record UpdateMessageViewModel(
        string Content,
        int TicketId);

    public sealed record MessageViewModel(
        int Id,
        string Content,
        string AuthorFullName,
        MessageStatus Status,
        DateTime PostDate);
}
