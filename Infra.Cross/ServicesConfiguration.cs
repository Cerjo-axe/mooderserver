using Domain.Interfaces;
using Infra.Data.Repository;
using Microsoft.Extensions.DependencyInjection;
using Service.Services;

namespace Infra.Cross;

public static class ServicesConfiguration
{
    public static void AddCustomDependency(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(MooderProfile));
        services.AddScoped<IMoodDayRepository,MoodDayRepository>();
        services.AddScoped<IMoodDayService,MoodDayService>();
    }
}
