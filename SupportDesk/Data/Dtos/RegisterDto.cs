namespace SupportDesk.Data.Dtos
{
    public sealed record RegisterDto(
        string FirstName,
        string Infix,
        string LastName,
        string Email,
        string Password);
}