using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingGain.Api.Domain.Models;

namespace TrainingGain.Api.Domain.Repositories
{
    public interface IHistoryRepository
    {
        Task<IEnumerable<History>> ListAsync();
        Task<IEnumerable<History>> ListByCustomerIdAsync(int customerId);
        Task<IEnumerable<History>> ListBySessionIdAsync(int sessionId);
        Task AddAsync(History history);
        void Remove(History history);   
    }
}
