
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repository;

namespace Restaurants.Application.Dishes.Commands.CreateDish
{
    public class CreateDishCommandHandler(ILogger<CreateDishCommandHandler> logger, IRestaurantRepository restaurantRepository, IMapper mapper, IDishesRepository dishRepo,IRestaurantAuthorizationService restaurantAuthorizationService) : IRequestHandler<CreateDishCommand, int>
    {
        public async Task<int> Handle(CreateDishCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Creating new dish :{@DishRequest}", request);
            var restaurant = await restaurantRepository.GetByIdAsync(request.RestaurantId);
            if (restaurant == null)
            {
                throw new NotFoundException($"Restaurant with the id:{request.RestaurantId} does't exist");
            }

            if (!restaurantAuthorizationService.Authorize(restaurant, ResourceOperation.Update))
                throw new ForbidException();

            var dish = mapper.Map<Dish>(request);

           return await dishRepo.Create(dish);
        }


    }
}
