using SimpleBlog.Data;

namespace SimpleBlog.Extensions
{
    public static class UnitOfWorkExtensions
    {
        public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
        {
            services.AddTransient<ApplicationDbContext>();
            services.AddTransient<UnitOfWork>();

            return services;
        }
    }
}
