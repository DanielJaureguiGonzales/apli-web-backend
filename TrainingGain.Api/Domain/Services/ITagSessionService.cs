using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingGain.Api.Domain.Models;
using TrainingGain.Api.Domain.Services.Communication;

namespace TrainingGain.Api.Domain.Services
{
    public interface ITagSessionService
    {
        Task<IEnumerable<TagSession>> ListAsync();
        Task<IEnumerable<TagSession>> ListByTagIdAsync(int tagId);      
        Task<IEnumerable<TagSession>> ListBySessionIdAsync(int sessionId);
        Task<TagSessionResponse> AssignTagSessionAsync(int tagId, int sessionId);                                         
    
    }
}
