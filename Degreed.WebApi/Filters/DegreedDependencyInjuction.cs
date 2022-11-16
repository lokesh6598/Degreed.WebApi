using Degreed.BAL;
using Degreed.ICanHazDadJoke;
using Degreed.Logger;
using Microsoft.Extensions.DependencyInjection;

namespace Degreed.WebApi.Filters
{
    public class DegreedDependencyInjuction
    {
        public static void PrepareDependencyInjection(IServiceCollection services)
        {
            services.AddTransient<IDegreedLogger, DegreedLogger>();
            services.AddTransient<IDadJokeService, DadJokeService>();
            services.AddTransient<IDadJokeExternalService, DadJokeExternalService>();
        }
    }
}
