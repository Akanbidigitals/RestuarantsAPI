using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Dishes;
using Restaurants.Application.Dishes.Commands.CreateDish;
using Restaurants.Application.Dishes.Commands.DeleteDish;
using Restaurants.Application.Dishes.Queries;
using Restaurants.Application.Dishes.Queries.GetAllDishes;
using Restaurants.Application.Dishes.Queries.GetDishById;
using Restaurants.Application.Restaurants.Commands.DeleteRestaurant;
using Restaurants.Infrastructure.Authorization;

namespace Restaurants.API.Controllers
{
    [Route("api/restaurants/{restaurantId}/dishes")]
    [ApiController]
    [Authorize]
    public class DishesController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult>CreateDish([FromRoute]int restaurantId, CreateDishCommand command)
        {

            command.RestaurantId = restaurantId;
           var dishId =  await mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { restaurantId, dishId }, null); ;
        }
        [HttpGet]
        [Authorize(Policy = PolicyNames.AtLeast20)]
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


        [HttpDelete("{dishId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeletebyId([FromRoute] int restaurantId,int dishId)
        {
            await mediator.Send(new DeleteDishCommand (restaurantId,dishId));
            return NoContent();


        }
    }
}
