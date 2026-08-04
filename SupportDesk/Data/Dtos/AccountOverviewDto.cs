namespace SupportDesk.Data.Dtos
{
    public sealed record AccountOverviewDto(
        string FullName,
        string Email,
        string Role,
        DateOnly CreationDate);
}