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
    public class TagSessionRepository : BaseRepository, ITagSessionRepository
    {
        public TagSessionRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(TagSession tagSession)
        {
            await _context.TagSessions.AddAsync(tagSession);
        }

        public async Task AssignTagSessionAsync(int tagId, int sessionId)
        {
            TagSession tagSession = await FindByTagIdAndSessionId(tagId, sessionId);
            if (tagSession == null)
            {
                tagSession = new TagSession { TagId = tagId, SessionId = sessionId};
                await AddAsync(tagSession);
            }
           
        }

        public async Task<TagSession> FindByTagIdAndSessionId(int tagId, int sessionId)
        {
            return await _context.TagSessions.FindAsync(tagId, sessionId);
        }

        public async Task<IEnumerable<TagSession>> ListAsync()
        {
            return await _context.TagSessions.Include(ts => ts.Tag).Include(ts => ts.Session).ToListAsync();

        }

        public async Task<IEnumerable<TagSession>> ListBySessionIdAsync(int sessionId)
        {
            return await _context.TagSessions.Where(ts => ts.SessionId == sessionId).Include(ts => ts.Tag).Include(ts => ts.Session).ToListAsync();

        }

        public async Task<IEnumerable<TagSession>> ListByTagIdAsync(int tagId)
        {
            return await _context.TagSessions.Where(ts => ts.TagId == tagId).Include(ts => ts.Tag).Include(ts => ts.Session).ToListAsync();

        }

        public void Remove(TagSession tagSession)
        {
            _context.TagSessions.Remove(tagSession);
        }
    }
}
