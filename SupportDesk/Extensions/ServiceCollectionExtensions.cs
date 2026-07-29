using SupportDesk.Services;

namespace SupportDesk.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(
            this IServiceCollection services)
        {
            services.AddScoped<TicketService>();

            return services;
        }
    }
}
