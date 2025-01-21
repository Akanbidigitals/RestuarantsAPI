using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Restaurants;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.Restaurants.Commands.DeleteRestaurant;
using Restaurants.Application.Restaurants.Commands.UpdateRestaurant;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Application.Restaurants.Queries.GetAllRestaurants;
using Restaurants.Application.Restaurants.Queries.GetRestaurantById;

namespace Restaurants.API.Controllers
{
    [ApiController]
    [Route("restaurants")]
    public class RestaurantsController(IMediator mediator) : Controller
    {

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var restaurants = await mediator.Send(new GetAllRestaurantsQuery());
            return Ok(restaurants);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var res = await mediator.Send(new GetRestaurantByIdQuery(id));
            if (res is null)
            {
                return NotFound("Restaurant not in the Db");
            }
            return Ok(res);
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> DeletebyId([FromRoute] int id)
        {
            var isDeleted = await mediator.Send(new DeleteRestaurantCommand(id));
            if (isDeleted) return NoContent();

            return NotFound();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateRestaurant([FromRoute] int id, UpdateRestaurantCommand command)
        {
            command.Id = id;
            var isUpdated = await mediator.Send(command);
            if (isUpdated) return NoContent();

            return NotFound();
        }       

        [HttpPost]
        public async Task<IActionResult> CreateRestaurant(CreateRestaurantCommand createCommand)
        {
            int id = await mediator.Send(createCommand);
            return CreatedAtAction(nameof(GetById), new { id }, null);
        }
    }
}
