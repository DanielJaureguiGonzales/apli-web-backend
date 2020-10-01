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
            await _context.Session.AddAsync(session);
        }

        public async Task<Session> FindById(int id)
        {
            return await _context.Session.FindAsync(id);
        }

        public async Task<IEnumerable<Session>> ListAsync()
        {
            return await _context.Session.ToListAsync();
        }

        public async Task<IEnumerable<Session>> ListAsyncByTittle(string tittle)
        {
            return await _context.Session.Where(pt => pt.Tittle == tittle).ToListAsync();
        }

        public void Remove(Session session)
        {
            _context.Session.Remove(session);
        }

        public void Update(Session session)
        {
            _context.Session.Update(session);
        }

       
    }
}
