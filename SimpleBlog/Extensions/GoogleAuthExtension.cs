using Google.Apis.Auth.AspNetCore3;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace SimpleBlog.Settings
{
    public static class GoogleAuthExtension
    {
        /// <summary>
        /// Reads config from configuration and setup authentication using google auth
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public static IServiceCollection AddGoogleAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            string? clientId = configuration["Authentication:Google:ClientId"];
            string? secret = configuration["Authentication:Google:Secret"];
            if (string.IsNullOrEmpty(clientId) || string.IsNullOrEmpty(secret))
                throw new ArgumentNullException("Invalid Google auth configuration");

            services.AddAuthentication()
                    .AddGoogleOpenIdConnect(options =>
                    {
                        options.ClientId = clientId;
                        options.ClientSecret = secret;
                    });

            return services;
        }
    }
}
