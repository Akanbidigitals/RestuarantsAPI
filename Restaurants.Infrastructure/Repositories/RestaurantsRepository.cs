using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repository;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Repositories
{
    internal class RestaurantsRepository(RestaurantDbContext _ctx) : IRestaurantRepository
    {
        public async Task<int> Create(Restaurant restaurant)
        {
            _ctx.Restaurants.Add(restaurant);
            await _ctx.SaveChangesAsync();
            return restaurant.Id;

        }

        public async Task Delete(Restaurant entity)
        {
            _ctx.Remove(entity);
            await _ctx.SaveChangesAsync();
        }

        public async Task<IEnumerable<Restaurant>> GetAllAsync()
        {
            var restaurants = await _ctx.Restaurants.ToListAsync();
            return restaurants;
        }

        public async Task<Restaurant?> GetByIdAsync(int id)
        {
            var restaurant = await _ctx.Restaurants.Include(r => r.Dishes).FirstOrDefaultAsync(x => x.Id == id);
            return restaurant;

        }

        public Task SaveToDb() => _ctx.SaveChangesAsync();
        
       
    }
}
