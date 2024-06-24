using PABlabZalApi.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PABlabZalApi.Core.Interfaces
{
    public interface IClientService
    {
        Task<Client> GetClientByIdAsync(int id);
        Task<IEnumerable<Client>> GetClientsAsync();
        Task AddClientAsync(Client client);
        Task UpdateClientAsync(Client client);
        Task DeleteClientAsync(int id);
    }
}
