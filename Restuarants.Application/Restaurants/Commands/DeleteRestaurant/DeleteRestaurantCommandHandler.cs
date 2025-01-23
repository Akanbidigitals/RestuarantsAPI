

using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repository;

namespace Restaurants.Application.Restaurants.Commands.DeleteRestaurant
{
    internal class DeleteRestaurantCommandHandler(ILogger<DeleteRestaurantCommandHandler> logger, IRestaurantRepository restaurantRepo) : IRequestHandler<DeleteRestaurantCommand>
    {
        public async Task Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Deleted restaurant with id {@RestaurantId}",request.Id);
            var restaurant = await restaurantRepo.GetByIdAsync(request.Id);
            if (restaurant is null)
                throw new NotFoundException($"Restaurant with the id : {request.Id} does't exist");

            
            await restaurantRepo.Delete(restaurant);
            

        }
    }
}
