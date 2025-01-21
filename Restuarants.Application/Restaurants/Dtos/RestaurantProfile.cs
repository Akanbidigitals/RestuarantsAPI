

using AutoMapper;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.Restaurants.Commands.UpdateRestaurant;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Restaurants.Dtos
{
    public class RestaurantProfile : Profile
    {
        public RestaurantProfile()
        {
            CreateMap<UpdateRestaurantCommand, Restaurant>();

            CreateMap<CreateRestaurantCommand, Restaurant>()
                .ForMember(d => d.Address, opt => opt.MapFrom(
                   
                    scr => new Address
                    {
                        City = scr.City,
                        PostalCode = scr.PostalCode,
                        Street = scr.Street,
                    } ));

            CreateMap<Restaurant, RestaurantDto>()
            .ForMember(d => d.City, opt => opt.MapFrom(scr => scr.Address == null ? null : scr.Address.City))
            .ForMember(d => d.Street, opt => opt.MapFrom(scr => scr.Address == null ? null : scr.Address.Street))
            .ForMember(d => d.PostalCode, opt => opt.MapFrom(scr => scr.Address == null ? null : scr.Address.PostalCode))
            //For Dishes
            .ForMember(d => d.Dishes, opt => opt.MapFrom(scr => scr.Dishes));
        }
    }
}
