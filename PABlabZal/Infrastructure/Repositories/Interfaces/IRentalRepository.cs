using System.Collections.Generic;
using System.Threading.Tasks;
using PABlabZalApi.Core.Entities;

namespace PABlabZalApi.Infrastructure.Repositories
{
    public interface IRentalRepository
    {
        Task<IEnumerable<Rental>> GetAllRentalsAsync();
        Task<Rental> GetRentalByIdAsync(int id);
        Task AddRentalAsync(Rental rental);
        Task UpdateRentalAsync(Rental rental);
        Task DeleteRentalAsync(int id);
    }
}
