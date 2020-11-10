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
    public class HistoryRepository : BaseRepository, IHistoryRepository
    {
        public HistoryRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(History history)
        {
            await _context.Histories.AddAsync(history);
        }

        public async Task AssingHistory(int customerId, int sessionId,DateTime Watched) 
        {
            History history = await FindByCustomerIdAndSessionId(customerId, sessionId);
            if (history==null)
            {
                history = new History { CustomerId = customerId, SessionId = sessionId , Watched=Watched};
                await AddAsync(history);
            }
            history.Watched = Watched;
        }

        public async Task<History> FindByCustomerIdAndSessionId(int customerId, int sessionId)
        {
            return await _context.Histories.FindAsync(customerId, sessionId);
        }

        public async Task<IEnumerable<History>> ListAsync()
        {
            return await _context.Histories.Include(s => s.Customer).Include(s => s.Session).ToListAsync();
        }

        public async Task<IEnumerable<History>> ListByCustomerIdAsync(int customerId)
        {
            return await _context.Histories.Where(s => s.CustomerId == customerId).Include(S => S.Customer).Include(S => S.Session).ToListAsync();

        }

        public async Task<IEnumerable<History>> ListBySessionIdAsync(int sessionId)
        {
            return await _context.Histories.Where(s => s.SessionId == sessionId).Include(S => S.Customer).Include(S => S.Session).ToListAsync();

        }

        public void Remove(History history)
        {
            _context.Histories.Remove(history);
        }
    }
}
