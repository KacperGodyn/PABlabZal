using System.Collections.Generic;
using System.Threading.Tasks;
using PABlabZalApi.Core.Entities;

namespace PABlabZalApi.Infrastructure.Repositories
{
    public interface ICarRepository
    {
        Task<IEnumerable<Car>> GetAllCarsAsync();
        Task<Car> GetCarByIdAsync(int id);
        Task AddCarAsync(Car car);
        Task UpdateCarAsync(Car car);
        Task DeleteCarAsync(int id);
        Task<IEnumerable<Car>> GetCarsByBrandAsync(string brand);
    }
}
