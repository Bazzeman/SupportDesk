namespace SupportDesk.Data.ViewModels
{
    public sealed record CreateMessageViewModel(
        string Content,
        int TicketId);

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
