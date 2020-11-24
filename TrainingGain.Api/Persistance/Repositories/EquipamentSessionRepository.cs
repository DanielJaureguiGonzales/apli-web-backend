using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingGain.Api.Domain.Models;
using TrainingGain.Api.Domain.Persistance.Context;
using TrainingGain.Api.Domain.Repositories;
using TrainingGain.Api.Domain.Services.Communication;

namespace TrainingGain.Api.Persistance.Repositories
{
    public class EquipamentSessionRepository : BaseRepository, IEquipamentSessionRepository
    {
        public EquipamentSessionRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(EquipamentSession EquipamentSession)
        {
            await _context.EquipamentSessions.AddAsync(EquipamentSession);
        }

        public async Task AssignEquipamentSessionAsync(int equipamentId, int sessionId)
        {
            EquipamentSession equipamentSession = await FindByEquipamentIdAndSessionId(equipamentId, sessionId);
            if (equipamentSession == null)
            {
                equipamentSession = new EquipamentSession { EquipamentId = equipamentId, SessionId = sessionId };
                await AddAsync(equipamentSession);
            }

        }

        public async Task<EquipamentSession> FindByEquipamentIdAndSessionId(int equipamentId, int sessionId)
        {
            return await _context.EquipamentSessions.FindAsync(equipamentId, sessionId);
        }

        public async Task<IEnumerable<EquipamentSession>> ListAsync()
        {
            return await _context.EquipamentSessions.Include(es => es.Equipament).Include(es => es.Session).ToListAsync();
        }

        public async Task<IEnumerable<EquipamentSession>> ListByEquipamentIdAsync(int equipamentId)
        {
            return await _context.EquipamentSessions.Where(es => es.EquipamentId == equipamentId).Include(es => es.Equipament).Include(es => es.Session).ToListAsync();
        }

        public async Task<IEnumerable<EquipamentSession>> ListBySessionIdAsync(int sessionId)
        {
            return await _context.EquipamentSessions.Where(es => es.SessionId == sessionId).Include(es => es.Equipament).Include(es => es.Session).ToListAsync(); 

        }

        public void Remove(EquipamentSession EquipamentSession)
        {
            _context.EquipamentSessions.Remove(EquipamentSession);
        }

     
        public async void UnassignEquipamentSessionAsync(int equipamentId, int sessionId)
        {
            EquipamentSession equipamentSession = await FindByEquipamentIdAndSessionId(equipamentId, sessionId);
            if (equipamentSession != null)
            {
                Remove(equipamentSession);
            }
        }
    }
}
