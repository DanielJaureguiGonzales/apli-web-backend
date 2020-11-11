using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingGain.Api.Domain.Models;
using TrainingGain.Api.Domain.Services.Communication;

namespace TrainingGain.Api.Domain.Repositories
{
    public interface IEquipamentSessionRepository
    {
        Task<IEnumerable<EquipamentSession>> ListAsync();
        Task<IEnumerable<EquipamentSession>> ListByEquipamentIdAsync(int equipamentId);    
        Task<IEnumerable<EquipamentSession>> ListBySessionIdAsync(int sessionId);
        Task<EquipamentSession> FindByEquipamentIdAndSessionId(int equipamentId, int sessionId);    
        Task AddAsync(EquipamentSession EquipamentSession);
        void Remove(EquipamentSession EquipamentSession);   
        Task AssignEquipamentSessionAsync(int equipamentId, int sessionId);
        void UnassignEquipamentSessionAsync(int equipamentId, int sessionId);    
    }
}
