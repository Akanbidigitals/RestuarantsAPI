using Restaurants.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Domain.Repository
{
    public interface IRestaurantRepository
    {
        Task<IEnumerable<Restaurant>> GetAllAsync();
        Task<Restaurant?>GetByIdAsync(int id);

        Task Delete(Restaurant entity);
        Task <int>Create(Restaurant restaurant);

        Task SaveToDb();
    }
}
