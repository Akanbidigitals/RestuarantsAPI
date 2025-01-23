using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repository;

namespace Restaurants.Application.Dishes.Queries.GetDishById
{
    internal class GetDishByIdForRestaurantQueryHandler(ILogger<GetDishByIdForRestaurantQueryHandler>logger,IMapper mapper,IRestaurantRepository restaurantRepository,IDishesRepository dishesRepository) : IRequestHandler<GetDishByIdForRestaurantQuery, DishDto>
    {
        public async Task<DishDto> Handle(GetDishByIdForRestaurantQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting Dishes of Id{DishId} under restaurant Id {@restaurantId}", request.DishId, request.RestaurantId);
            var restaurant = await restaurantRepository.GetByIdAsync(request.RestaurantId);
            if (restaurant == null)
            {
                throw new NotFoundException($"The restaurant Id:{request.RestaurantId} does not exist");
            }
            var dish = restaurant.Dishes.FirstOrDefault(x=>x.Id == request.DishId);
            if(dish == null) throw new NotFoundException($"The dish Id:{request.DishId} does not exist");

            var result = mapper.Map<DishDto>(dish);
            return result;
        }
    }
}
