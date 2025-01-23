using MediatR;

namespace Restaurants.Application.Dishes.Queries.GetDishById
{
    public class GetDishByIdForRestaurantQuery(int resturantId, int dishId):IRequest<DishDto>
    {
       public int RestaurantId {  get; set; } = resturantId;
        public int DishId { get; set; } = dishId;
    }
}
