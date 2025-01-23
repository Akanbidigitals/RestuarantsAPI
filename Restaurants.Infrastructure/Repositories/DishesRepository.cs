
using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repository;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Repositories
{
    internal class DishesRepository(RestaurantDbContext _ctx) : IDishesRepository
    {
      
        public async Task<int> Create(Dish entity)
        {
             _ctx.Dishes.Add(entity);
            await _ctx.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<IEnumerable<Dish>> GetAllAsync()
        {
            var dishes = await _ctx.Dishes.ToListAsync();
            return dishes;
        }

        public async Task<Dish?> GetById(int id)
        {
            var dish = await _ctx.Dishes.FirstOrDefaultAsync(x => x.Id == id);
            return dish;
        }
    }
}
