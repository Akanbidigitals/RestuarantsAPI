using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repository;
using Restaurants.Infrastructure.Authorization;
using Restaurants.Infrastructure.Authorization.Requirements;
using Restaurants.Infrastructure.Authorization.Services;
using Restaurants.Infrastructure.Persistence;
using Restaurants.Infrastructure.Repositories;
using Restaurants.Infrastructure.Seeders;


namespace Restaurants.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("RestaurantDb");
            services.AddDbContext<RestaurantDbContext>(options => options.UseSqlServer(connectionString).EnableSensitiveDataLogging());

            services.AddIdentityApiEndpoints<User>()
                .AddRoles<IdentityRole>()
                .AddClaimsPrincipalFactory<RestaurantsUserClaimPrincipalFactory>()
                .AddEntityFrameworkStores<RestaurantDbContext>();


            services.AddScoped<IRestaurantSeeder, RestaurantSeeder>();
            services.AddScoped<IRestaurantRepository, RestaurantsRepository>();
            services.AddScoped<IDishesRepository, DishesRepository>();
            //Add Authorization policy
            services.AddAuthorizationBuilder()
                .AddPolicy(PolicyNames.HasNationality,
                builder => builder.RequireClaim(AppClaimTtpes.Nationality, "Ausie"))
                .AddPolicy(PolicyNames.AtLeast20,
                builder => builder.AddRequirements(new MinimumAgeRequirement(50)))
                .AddPolicy(PolicyNames.CreatedAtleast2Restaurants,
                builder => builder.AddRequirements(new CreateMultipleRestaurantsRequirement(2)));

            services.AddScoped<IAuthorizationHandler,MinimumAgeRequirementHandler >();
            services.AddScoped<IAuthorizationHandler,CreateMultipleRestaurantsRequirementHandler>();
            services.AddScoped<IRestaurantAuthorizationService, RestaurantAuthorizationService>();
        }
    }
}
