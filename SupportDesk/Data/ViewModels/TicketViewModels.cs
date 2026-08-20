namespace SupportDesk.Data.ViewModels
{
    public sealed record CreateTicketViewModel(
        string Title,
        string Description);

    public sealed record TicketViewModel(
        string Title,
        string Description,
        DateTime CreationDate,
        IEnumerable<MessageViewModel> Messages,
        CreateMessageViewModel NewMessage);

    public sealed record TicketOverviewViewModel(
        int Id,
        string Title,
        string Description,
        DateTime CreationDate);
}
