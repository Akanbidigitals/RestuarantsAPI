

using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Repository;

namespace Restaurants.Application.Restaurants.Commands.DeleteRestaurant
{
    internal class DeleteRestaurantCommandHandler(ILogger<DeleteRestaurantCommandHandler> logger, IRestaurantRepository restaurantRepo) : IRequestHandler<DeleteRestaurantCommand,bool>
    {
        public async Task<bool> Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation($"Deleted restaurant with id {request.Id}");
            var restaurant = await restaurantRepo.GetByIdAsync(request.Id);
            if (restaurant is null) return false;
            
            await restaurantRepo.Delete(restaurant);
            return true;

        }
    }
}
