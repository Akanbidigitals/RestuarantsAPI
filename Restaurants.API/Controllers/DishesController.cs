using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Dishes;
using Restaurants.Application.Dishes.Commands;
using Restaurants.Application.Dishes.Queries;
using Restaurants.Application.Dishes.Queries.GetAllDishes;
using Restaurants.Application.Dishes.Queries.GetDishById;

namespace Restaurants.API.Controllers
{
    [Route("api/restaurants/{restaurantId}/dishes")]
    [ApiController]
    public class DishesController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult>CreateDish([FromRoute]int restaurantId, CreateDishCommand command)
        {

            command.RestaurantId = restaurantId;
            await mediator.Send(command);
            return Created();
        }
        [HttpGet]
       public async Task<ActionResult<IEnumerable<DishDto>>> GetAll([FromRoute] int restaurantId)
        {
            var dishes = await mediator.Send(new GetDishesForRestaurantQuery(restaurantId));
            return Ok(dishes);
        }
        
        [HttpGet("{dishId}")]
       public async Task<ActionResult<DishDto>> GetById([FromRoute] int restaurantId,int dishId)
        {
            var dish = await mediator.Send(new GetDishByIdForRestaurantQuery(restaurantId,dishId));
            return Ok(dish);
        }
    }
}
