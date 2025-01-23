
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repository;

namespace Restaurants.Application.Restaurants.Queries.GetRestaurantById
{
    internal class GetRestaurantByIdQueryHandler(ILogger<GetRestaurantByIdQueryHandler> logger, IRestaurantRepository restaurantRepo,IMapper mapper) : IRequestHandler<GetRestaurantByIdQuery, RestaurantDto>
    {
        public async Task<RestaurantDto> Handle(GetRestaurantByIdQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting restaurant {@RequestId}",request);

            var restaurant = await restaurantRepo.GetByIdAsync(request.Id);
            if(restaurant is null)
            {

            throw new NotFoundException($"Restaurant with the id :{request.Id} does't exist");
            }

            var restaurantDto = mapper.Map<RestaurantDto>(restaurant);

            // var restaurantDto = RestaurantDto.FromEntity(restaurant);  // Using Dto
            return restaurantDto;
        }
    }
}
