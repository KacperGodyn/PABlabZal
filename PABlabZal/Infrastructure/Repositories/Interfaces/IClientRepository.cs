using System.Collections.Generic;
using System.Threading.Tasks;
using PABlabZalApi.Core.Entities;

namespace PABlabZalApi.Infrastructure.Repositories
{
    public interface IClientRepository
    {
        Task<IEnumerable<Client>> GetAllClientsAsync();
        Task<Client> GetClientByIdAsync(int id);
        Task AddClientAsync(Client client);
        Task UpdateClientAsync(Client client);
        Task DeleteClientAsync(int id);
    }
}
