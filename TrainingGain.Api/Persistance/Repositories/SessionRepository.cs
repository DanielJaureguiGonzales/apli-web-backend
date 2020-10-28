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
    public class SessionRepository : BaseRepository, ISessionRepository
    {
        public SessionRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(Session session)
        {
            await _context.Sessions.AddAsync(session);
        }

        public async Task<Session> FindById(int id)
        {
            return await _context.Sessions.FindAsync(id);
        }

        public async Task<IEnumerable<Session>> ListAsync()
        {
            return await _context.Sessions.Include(s=>s.Specialist).ToListAsync();
        }

        public async Task<IEnumerable<Session>> ListAsyncBySpecialistId(int specialistId)
        {
            return await _context.Sessions.Where(s => s.SpecialistId == specialistId).Include(s => s.Specialist).ToListAsync();
        }

        public async Task<IEnumerable<Session>> ListAsyncByTittle(string tittle)
        {
            return await _context.Sessions.Where(pt => pt.Tittle == tittle).Include(s => s.Specialist).ToListAsync();
        }

        public void Remove(Session session)
        {
            _context.Sessions.Remove(session);
        }

        public void Update(Session session)
        {
            _context.Sessions.Update(session);
        }

       
    }
}
