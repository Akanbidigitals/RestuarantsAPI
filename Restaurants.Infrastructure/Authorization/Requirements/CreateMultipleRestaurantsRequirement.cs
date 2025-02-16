using Microsoft.AspNetCore.Authorization;


namespace Restaurants.Infrastructure.Authorization.Requirements
{
    public class CreateMultipleRestaurantsRequirement(int minimumRestaurantCreated) : IAuthorizationRequirement
    {
        public int MinimumRestaurantCreated { get; } = minimumRestaurantCreated;
    }
}
