﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Restaurants;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.Restaurants.Commands.DeleteRestaurant;
using Restaurants.Application.Restaurants.Commands.UpdateRestaurant;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Application.Restaurants.Queries.GetAllRestaurants;
using Restaurants.Application.Restaurants.Queries.GetRestaurantById;
using Restaurants.Infrastructure.Authorization;

namespace Restaurants.API.Controllers
{
    [ApiController]
    [Route("/api/restaurants")]
   // [Authorize]
    public class RestaurantsController(IMediator mediator) : Controller
    {

        [HttpGet]

        [Authorize(Policy = PolicyNames.CreatedAtleast2Restaurants)]
       // [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<RestaurantDto>>> GetAll()
        {
            var restaurants = await mediator.Send(new GetAllRestaurantsQuery());
            return Ok(restaurants);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RestaurantDto>> GetById([FromRoute] int id)
        {
            var res = await mediator.Send(new GetRestaurantByIdQuery(id));
         
            return Ok(res);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeletebyId([FromRoute] int id)
        {
            await mediator.Send(new DeleteRestaurantCommand(id));
           return NoContent();

            
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateRestaurant([FromRoute] int id, UpdateRestaurantCommand command)
        {
            command.Id = id;
            await mediator.Send(command);


            return NoContent();
        }       

        [HttpPost]
        public async Task<IActionResult> CreateRestaurant(CreateRestaurantCommand createCommand)
        {
            int id = await mediator.Send(createCommand);
            return CreatedAtAction(nameof(GetById), new { id }, null);
        }
    }
}
