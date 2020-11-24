using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingGain.Api.Domain.Models;
using TrainingGain.Api.Domain.Services.Communication;

namespace TrainingGain.Api.Domain.Services
{
    public interface IEquipamentSessionService
    {
        Task<IEnumerable<EquipamentSession>> ListAsync();
        Task<IEnumerable<EquipamentSession>> ListByEquipamentIdAsync(int equipamentId);
        Task<IEnumerable<EquipamentSession>> ListBySessionIdAsync(int sessionId);          
        Task<EquipamentSessionResponse> AssignEquipamentSessionAsync(int equipamentId, int sessionId);
        Task<EquipamentSessionResponse> UnassignEquipamentSessionAsync(int equipamentId, int sessionId);    
    }
}
