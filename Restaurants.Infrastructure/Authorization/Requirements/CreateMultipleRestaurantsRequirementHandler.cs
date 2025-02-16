
using Microsoft.AspNetCore.Authorization;
using Restaurants.Application.Users;
using Restaurants.Domain.Repository;

namespace Restaurants.Infrastructure.Authorization.Requirements
{
    public class CreateMultipleRestaurantsRequirementHandler(IRestaurantRepository restaurantRepository , IUserContext userContext) : AuthorizationHandler<CreateMultipleRestaurantsRequirement>
    {
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, CreateMultipleRestaurantsRequirement requirement)
        {
            var currentUser = userContext.GetCurrentUser();

            var restaurants  = await restaurantRepository.GetAllAsync();

            var userRestaurantsCreated = restaurants.Count(r => r.OwnerId == currentUser!.Id);

            if(userRestaurantsCreated >= requirement.MinimumRestaurantCreated)
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }
        }
    }
}
