using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingGain.Api.Domain.Models;
using TrainingGain.Api.Domain.Services.Communication;

namespace TrainingGain.Api.Domain.Services
{
    public interface ISessionService
    {
        Task<IEnumerable<Session>> ListAsync();
        Task<IEnumerable<Session>> ListAsyncByTittle(string tittle);
        Task<IEnumerable<Session>> ListBySpecialistIdAsync(int specialistId);
        Task<IEnumerable<Session>> ListByCustomerIdAsync(int customerId);
        Task<SessionResponse> SaveAsync(Session session);
        Task<SessionResponse> UpdateAsync(int id, Session session);
        Task<SessionResponse> DeleteAsync(int id);
    }
}
