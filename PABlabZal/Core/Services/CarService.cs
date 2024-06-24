using PABlabZalApi.Core.Entities;
using PABlabZalApi.Core.Interfaces;
using PABlabZalApi.Infrastructure.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PABlabZalApi.Core.Services
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _carRepository;

        public CarService(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public async Task<Car> GetCarByIdAsync(int id)
        {
            return await _carRepository.GetCarByIdAsync(id);
        }

        public async Task<IEnumerable<Car>> GetCarsAsync()
        {
            return await _carRepository.GetAllCarsAsync();
        }

        public async Task AddCarAsync(Car car)
        {
            await _carRepository.AddCarAsync(car);
        }

        public async Task UpdateCarAsync(Car car)
        {
            await _carRepository.UpdateCarAsync(car);
        }

        public async Task DeleteCarAsync(int id)
        {
            await _carRepository.DeleteCarAsync(id);
        }
    }
}
