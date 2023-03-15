using Domain.Entity;
using Domain.Interfaces;
using Infra.Data.Context;
using Infra.Data.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Service.Services;

namespace Infra.Cross;

public static class ServicesConfiguration
{
    public static void AddCustomDependency(this IServiceCollection services)
    {
        //add identity
        services.AddIdentity<User,IdentityRole>().AddEntityFrameworkStores<AppDbContext>()
        .AddDefaultTokenProviders();

        //automapper
        services.AddAutoMapper(typeof(MooderProfile));

        //addservices
        services.AddScoped<IMoodDayRepository,MoodDayRepository>();
        services.AddScoped<IMoodDayService,MoodDayService>();
        services.AddScoped<IUserService,UserService>();
    }
}
