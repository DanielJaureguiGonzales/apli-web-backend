using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingGain.Api.Domain.Models;
using TrainingGain.Api.Domain.Persistance.Context;
using TrainingGain.Api.Domain.Repositories;

namespace TrainingGain.Api.Persistance.Repositories
{
    public class SpecialistRepository : BaseRepository, ISpecialistRepository
    {
        public SpecialistRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(Specialist specialist)
        {
            await _context.Specialist.AddAsync(specialist);
        }

        public async Task<Specialist> FindById(int id)
        {
            return await _context.Specialist.FindAsync(id);
        }

        public async Task<IEnumerable<Specialist>> ListAsync()
        {
            return await _context.Specialist.ToListAsync();
        }

        public void Remove(Specialist specialist)
        {
            _context.Specialist.Remove(specialist);
        }

        public void Update(Specialist specialist)
        {
            _context.Specialist.Update(specialist);
        }
    }
}
