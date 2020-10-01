using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingGain.Api.Domain.Models;

namespace TrainingGain.Api.Domain.Repositories
{
    public interface ISessionRepository
    {
        Task<IEnumerable<Session>> ListAsync();
        Task<IEnumerable<Session>> ListAsyncByTittle(string tittle);
        Task AddAsync(Session session);
        Task<Session> FindById(int id);
        void Update(Session session);
        void Remove(Session session);
    }
}
