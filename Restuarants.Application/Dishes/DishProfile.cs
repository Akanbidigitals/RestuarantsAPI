using AutoMapper;
using Restaurants.Application.Dishes.Commands;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Dishes
{
    public class DishProfile : Profile
    {
        public DishProfile()
        {
            CreateMap<CreateDishCommand, Dish>();
             CreateMap<Dish, DishDto>();
        }
    }
}
