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
            await _context.Specialists.AddAsync(specialist);
        }

        public async Task<Specialist> FindById(int id)
        {
            return await _context.Specialists.FindAsync(id);
        }

        public async Task<IEnumerable<Specialist>> ListAsync()
        {
            return await _context.Specialists.ToListAsync();
        }

        public void Remove(Specialist specialist)
        {
            _context.Specialists.Remove(specialist);
        }

        public void Update(Specialist specialist)
        {
            _context.Specialists.Update(specialist);
        }
    }
}
