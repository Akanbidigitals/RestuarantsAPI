

using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Users;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repository;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant
{
    public class CreateRestaurantCommandHandler(IMapper mapper,IRestaurantRepository restaurantRepo,ILogger<CreateRestaurantCommandHandler> logger,IUserContext userContext) : IRequestHandler<CreateRestaurantCommand, int>
    {
        public async Task<int> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
        {
            var currentUser = userContext.GetCurrentUser();

            logger.LogInformation("{UserEmail}[{UserId}] is creating a new {@Restaurant}",currentUser.Email,currentUser.Id,request);

            var restaurant = mapper.Map<Restaurant>(request);

            restaurant.OwnerId = currentUser.Id;

            int id = await restaurantRepo.Create(restaurant);
            return id;
        }
    }
}
