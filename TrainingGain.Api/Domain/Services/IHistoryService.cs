using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingGain.Api.Domain.Models;
using TrainingGain.Api.Domain.Services.Communication;

namespace TrainingGain.Api.Domain.Services
{
    public interface IHistoryService
    {
        Task<IEnumerable<History>> ListAsync();
        Task<IEnumerable<History>> ListByCustomerIdAsync(int customerId);
        Task<IEnumerable<History>> ListBySessionIdAsync(int sessionId);
        Task<HistoryResponse> AssignHistoryAsync(int customerId, int sessionId, DateTime Watched);    
    }
}
