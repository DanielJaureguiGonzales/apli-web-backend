using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingGain.Api.Domain.Models;
using TrainingGain.Api.Domain.Services.Communication;

namespace TrainingGain.Api.Domain.Services
{
    public interface ICustomerService
    {
        Task<IEnumerable<Customer>> ListAsync();
        Task<IEnumerable<Customer>> ListBySubscriptionPlanId(int subscriptionplanId);
        Task<IEnumerable<Customer>> ListBySessionIdAsync(int sessionId);
        Task<IEnumerable<Customer>> ListBySpecialistIdAsync(int specialistId);  
        Task<CustomerResponse> GetByIdAsync(int id);
        Task<CustomerResponse> SaveAsync(Customer customer);
        Task<CustomerResponse> UpdateAsync(int id, Customer customer);
        Task<CustomerResponse> DeleteAsync(int id);
    }
}
