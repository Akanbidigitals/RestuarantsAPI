using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repository;

namespace Restaurants.Application.Dishes.Queries.GetAllDishes
{
    public class GetDishesForRestaurantHandler(ILogger<GetDishesForRestaurantHandler> logger, IRestaurantRepository restaurantRepository, IMapper mapper, IDishesRepository dishesRepository) : IRequestHandler<GetDishesForRestaurantQuery, IEnumerable<DishDto>>
    {
        public async Task<IEnumerable<DishDto>> Handle(GetDishesForRestaurantQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting all dishes with restaurant id  {@RestauraniID}", request.RestaurantId);
            var restaurant = await restaurantRepository.GetByIdAsync(request.RestaurantId);
            if (restaurant == null)
            {
                throw new NotFoundException($"The restaurant Id:{request.RestaurantId} does not exist");
            }



            var dishDtos = mapper.Map<IEnumerable<DishDto>>(restaurant.Dishes);
            return dishDtos;




        }
    }
}
