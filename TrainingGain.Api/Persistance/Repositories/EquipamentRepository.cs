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
    public class EquipamentRepository : BaseRepository, IEquipamentRepository
    {
        public EquipamentRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(Equipament equipament)
        {
            await _context.Equipaments.AddAsync(equipament);
        }

        public async Task<Equipament> FindById(int id)
        {
           return await _context.Equipaments.FindAsync(id);
        }

        public async Task<IEnumerable<Equipament>> ListAsync()
        {
            return await _context.Equipaments.ToListAsync();
        }

        public  void Remove(Equipament equipament)
        {
            _context.Equipaments.Remove(equipament);
        }

        public void Update(Equipament equipament)
        {
            _context.Equipaments.Update(equipament);
        }
    }
}
