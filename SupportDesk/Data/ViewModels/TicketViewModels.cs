namespace SupportDesk.Data.ViewModels
{
    public sealed record CreateTicketViewModel(
        string Title,
        string Description);

    public sealed record TicketViewModel(
        string Title,
        string Description,
        DateTime CreationDate);

    public sealed record TicketOverviewViewModel(
        int Id,
        string Title,
        string Description,
        DateTime CreationDate);
}
