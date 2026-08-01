namespace SupportDesk.Data.ViewModels
{
    public sealed record LoginViewModel(
        string Email, 
        string Password,
        bool RememberMe);

    public sealed record RegisterViewModel(
        string FirstName,
        string Infix,
        string LastName,
        string Email,
        string Password);

    public sealed record AccountViewModel(
        string FullName,
        string Email,
        string Role);
}