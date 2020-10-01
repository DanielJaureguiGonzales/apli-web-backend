using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingGain.Api.Domain.Models;
using TrainingGain.Api.Domain.Services.Communication;

namespace TrainingGain.Api.Domain.Services
{
    public interface ISpecialistService
    {
        Task<IEnumerable<Specialist>> ListAsync();
        Task<SpecialistResponse> GetByIdAsync(int id);
        Task<SpecialistResponse> SaveAsync(Specialist specialist);
        Task<SpecialistResponse> UpdateAsync(int id, Specialist specialist);
        Task<SpecialistResponse> DeleteAsync(int id);


    }
}
