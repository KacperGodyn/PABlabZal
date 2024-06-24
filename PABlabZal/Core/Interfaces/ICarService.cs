using PABlabZalApi.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PABlabZalApi.Core.Interfaces
{
    public interface ICarService
    {
        Task<Car> GetCarByIdAsync(int id);
        Task<IEnumerable<Car>> GetCarsAsync();
        Task AddCarAsync(Car car);
        Task UpdateCarAsync(Car car);
        Task DeleteCarAsync(int id);
    }
}