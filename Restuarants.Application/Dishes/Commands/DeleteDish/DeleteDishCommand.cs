using MediatR;
using Restaurants.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Dishes.Commands.DeleteDish
{
    public class DeleteDishCommand(int resturantId, int dishId) : IRequest
    {
        public int RestaurantId { get; set; } = resturantId;
        public int DishId { get; set; } = dishId;
    }
}
