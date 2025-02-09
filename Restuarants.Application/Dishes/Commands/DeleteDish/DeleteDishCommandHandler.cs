using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Dishes.Commands.DeleteDish
{
    internal class DeleteDishCommandHandler(ILogger<DeleteDishCommandHandler>logger,IRestaurantRepository restaurantRepository,IDishesRepository dishesRepository,IRestaurantAuthorizationService restaurantAuthorizationService) : IRequestHandler<DeleteDishCommand>
    {
        public async Task Handle(DeleteDishCommand request, CancellationToken cancellationToken)
        {
            logger.LogWarning("Getting Dishes of Id{DishId} under restaurant Id {@restaurantId}", request.DishId, request.RestaurantId);
            var restaurant = await restaurantRepository.GetByIdAsync(request.RestaurantId);
            if (restaurant == null)
            {
                throw new NotFoundException($"The restaurant Id:{request.RestaurantId} does not exist");
            }
            var dish = restaurant.Dishes.FirstOrDefault(x => x.Id == request.DishId);
            if (dish == null) throw new NotFoundException($"The dish Id:{request.DishId} does not exist");


            if (!restaurantAuthorizationService.Authorize(restaurant, ResourceOperation.Update))
                throw new ForbidException();

            await dishesRepository.Delete(dish);
            
            
        }
    }
}
