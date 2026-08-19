using SupportDesk.Services;

namespace SupportDesk.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(
            this IServiceCollection services)
        {
            services.AddScoped<AccountService>();
            services.AddScoped<TicketService>();
            services.AddScoped<MessageService>();

            return services;
        }
    }
}
