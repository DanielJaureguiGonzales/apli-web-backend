using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingGain.Api.Domain.Models;

namespace TrainingGain.Api.Domain.Repositories
{
    public interface ISpecialistRepository
    {
        Task<IEnumerable<Specialist>> ListAsync();
        Task AddAsync(Specialist specialist);
        Task<Specialist> FindById(int id);
        void Update(Specialist specialist);
        void Remove(Specialist specialist);

    }
}
