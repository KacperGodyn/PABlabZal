using PABlabZalApi.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PABlabZalApi.Core.Interfaces
{
    public interface IRentalService
    {
        Task<Rental> GetRentalByIdAsync(int id);
        Task<IEnumerable<Rental>> GetRentalsAsync();
        Task AddRentalAsync(Rental rental);
        Task UpdateRentalAsync(Rental rental);
        Task DeleteRentalAsync(int id);
    }
}
