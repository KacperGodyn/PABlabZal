using PABlabZalApi.Core.Entities;
using PABlabZalApi.Core.Interfaces;
using PABlabZalApi.Infrastructure.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PABlabZalApi.Core.Services
{
    public class RentalService : IRentalService
    {
        private readonly IRentalRepository _rentalRepository;

        public RentalService(IRentalRepository rentalRepository)
        {
            _rentalRepository = rentalRepository;
        }

        public async Task<Rental> GetRentalByIdAsync(int id)
        {
            return await _rentalRepository.GetRentalByIdAsync(id);
        }

        public async Task<IEnumerable<Rental>> GetRentalsAsync()
        {
            return await _rentalRepository.GetAllRentalsAsync();
        }

        public async Task AddRentalAsync(Rental rental)
        {
            await _rentalRepository.AddRentalAsync(rental);
        }
                public async Task UpdateRentalAsync(Rental rental)
        {
            await _rentalRepository.UpdateRentalAsync(rental);
        }

        public async Task DeleteRentalAsync(int id)
        {
            await _rentalRepository.DeleteRentalAsync(id);
        }
    }
}