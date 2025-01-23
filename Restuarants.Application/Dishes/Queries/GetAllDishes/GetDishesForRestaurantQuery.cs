

using MediatR;

namespace Restaurants.Application.Dishes.Queries.GetAllDishes
{
    public class GetDishesForRestaurantQuery(int id) : IRequest<IEnumerable<DishDto>>
    {
        public int RestaurantId { get; set; } = id;
    }
}
