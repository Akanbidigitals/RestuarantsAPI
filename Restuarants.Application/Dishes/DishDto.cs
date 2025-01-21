
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Dishes
{
    public class DishDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int? KiloCalories { get; set; }

        public static  DishDto FromEntity(Dish d)
        {
            return new DishDto()
            {
                Id = d.Id,
                Name = d.Name,
                Description = d.Description,
                Price = d.Price,
                KiloCalories = d.KiloCalories,
            };
        }
       
    }
}
