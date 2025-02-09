

using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repository;

namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant
{
    public class UpdateRestaurantCommandHandler(ILogger<UpdateRestaurantCommandHandler> logger, IMapper mapper, IRestaurantRepository restaurantRepo,IRestaurantAuthorizationService restaurantAuthorizationService) : IRequestHandler<UpdateRestaurantCommand>
    {
        public async Task Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Updating restaurant with id {@RestaurantId} with {@UpdatedRestaurant}",request.Id,request);
            var restaurant = await restaurantRepo.GetByIdAsync(request.Id);
            if (restaurant is null) 
                throw new NotFoundException($"Restaurant with the id : {request.Id} does't exist");


            if (!restaurantAuthorizationService.Authorize(restaurant, ResourceOperation.Update))
                throw new ForbidException();

            mapper.Map(request, restaurant);

            //restaurant.Name = request.Name;
            //restaurant.Description = request.Description;
            //restaurant.HasDelivery = request.HasDelivery;

            await restaurantRepo.SaveToDb();

            
        }
    }
}
