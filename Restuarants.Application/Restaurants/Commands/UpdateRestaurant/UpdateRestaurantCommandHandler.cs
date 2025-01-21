

using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Repository;

namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant
{
    public class UpdateRestaurantCommandHandler(ILogger<UpdateRestaurantCommandHandler> logger, IMapper mapper, IRestaurantRepository restaurantRepo) : IRequestHandler<UpdateRestaurantCommand, bool>
    {
        public async Task<bool> Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Updating restaurant with id {@RestaurantId} with {@UpdatedRestaurant}",request.Id,request);
            var restaurant = await restaurantRepo.GetByIdAsync(request.Id);
            if (restaurant is null) return false;

            mapper.Map(request, restaurant);

            //restaurant.Name = request.Name;
            //restaurant.Description = request.Description;
            //restaurant.HasDelivery = request.HasDelivery;

            await restaurantRepo.SaveToDb();

            return true;
        }
    }
}
