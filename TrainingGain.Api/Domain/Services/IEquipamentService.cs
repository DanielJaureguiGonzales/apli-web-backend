using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingGain.Api.Domain.Models;
using TrainingGain.Api.Domain.Services.Communication;

namespace TrainingGain.Api.Domain.Services
{
    public interface IEquipamentService
    {
        Task<IEnumerable<Equipament>> ListAsync();
        Task<IEnumerable<Equipament>> ListBySessionIdAsync(int sessionId);
        Task<EquipamentResponse> GetByIdAsync(int id);
        Task<EquipamentResponse> SaveAsync(Equipament equipament);  
        Task<EquipamentResponse> UpdateAsync(int id, Equipament equipament);    
        Task<EquipamentResponse> DeleteAsync(int id);
    }
}
