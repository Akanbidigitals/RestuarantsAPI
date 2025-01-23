

using Restaurants.Domain.Entities;

namespace Restaurants.Domain.Repository
{
    public interface IDishesRepository
    {
        Task<int> Create(Dish entity);
        Task<IEnumerable<Dish>> GetAllAsync();
        Task<Dish?> GetById(int id);

    }
}
