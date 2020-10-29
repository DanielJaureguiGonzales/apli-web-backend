using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingGain.Api.Domain.Models;

namespace TrainingGain.Api.Domain.Services
{
    public interface IHistoryService
    {
        Task<IEnumerable<History>> ListAsyn();
        Task<IEnumerable<History>> ListByCustomerIdAsync(int customerId);
        Task<IEnumerable<History>> ListBySessionIdAsync(int sessionId); 
    }
}
