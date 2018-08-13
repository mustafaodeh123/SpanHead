namespace SpanHead.VA.Web
{
    using Microsoft.Extensions.DependencyInjection;
    using SpanHead.VA.Business.Account;
    using SpanHead.VA.Business.Precinct;
    using SpanHead.VA.Business.Voters;
    using SpanHead.VA.Repository.Account;
    using SpanHead.VA.Repository.Precinct;
    using SpanHead.VA.Repository.Voters;
    using SpanHead.VA.Web.Auth;

    public static class ServiceExtensions
    {
        public static IServiceCollection RegisterServices(
            this IServiceCollection services)
        {
            services.AddScoped<IAppUserDao, AppUserDao>();
            services.AddScoped<IAccountManager, AccountManager>();
            services.AddScoped<IPrecinctDao, PrecinctDao>();
            services.AddScoped<IPrecinctManager, PrecinctManager>();
            services.AddScoped<IVoterDao, VoterDao>();
            services.AddScoped<IVoterManager, VoterManager>();
            services.AddScoped<IJWTFactory, JWTFactory>();

            return services;
        }
    }
}
