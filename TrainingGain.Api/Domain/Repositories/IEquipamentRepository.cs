using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingGain.Api.Domain.Models;

namespace TrainingGain.Api.Domain.Repositories
{
    public interface IEquipamentRepository
    {
        Task<IEnumerable<Equipament>> ListAsync();
        Task AddAsync(Equipament equipament);
        Task<Equipament> FindById(int id);
        void Update(Equipament equipament);
        void Remove(Equipament equipament); 
    }
}
