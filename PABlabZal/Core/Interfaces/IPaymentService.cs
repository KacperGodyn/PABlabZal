using PABlabZalApi.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PABlabZalApi.Core.Interfaces
{
    public interface IPaymentService
    {
        Task<Payment> GetPaymentByIdAsync(int id);
        Task<IEnumerable<Payment>> GetPaymentsAsync();
        Task AddPaymentAsync(Payment payment);
        Task UpdatePaymentAsync(Payment payment);
        Task DeletePaymentAsync(int id);
    }
}
