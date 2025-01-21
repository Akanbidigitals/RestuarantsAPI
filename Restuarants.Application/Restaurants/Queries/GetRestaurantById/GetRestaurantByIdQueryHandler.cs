
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domain.Repository;

namespace Restaurants.Application.Restaurants.Queries.GetRestaurantById
{
    internal class GetRestaurantByIdQueryHandler(ILogger<GetRestaurantByIdQueryHandler> logger, IRestaurantRepository restaurantRepo,IMapper mapper) : IRequestHandler<GetRestaurantByIdQuery, RestaurantDto?>
    {
        public async Task<RestaurantDto?> Handle(GetRestaurantByIdQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting restaurant {@RequestId}",request);
            var restaurant = await restaurantRepo.GetByIdAsync(request.Id);

            var restaurantDto = mapper.Map<RestaurantDto?>(restaurant);

            // var restaurantDto = RestaurantDto.FromEntity(restaurant);  // Using Dto
            return restaurantDto;
        }
    }
}
