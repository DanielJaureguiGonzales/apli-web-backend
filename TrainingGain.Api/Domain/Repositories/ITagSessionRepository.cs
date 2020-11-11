using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingGain.Api.Domain.Models;
using TrainingGain.Api.Domain.Services.Communication;

namespace TrainingGain.Api.Domain.Repositories
{
    public interface ITagSessionRepository
    {
        Task<IEnumerable<TagSession>> ListAsync();
        Task<IEnumerable<TagSession>> ListByTagIdAsync(int tagId);  
        Task<IEnumerable<TagSession>> ListBySessionIdAsync(int sessionId);
        Task<TagSession> FindByTagIdAndSessionId(int tagId, int sessionId); 
        Task AddAsync(TagSession tagSession);
        void Remove(TagSession tagSession);
        Task AssignTagSessionAsync(int tagId, int sessionId);       
    }
}
