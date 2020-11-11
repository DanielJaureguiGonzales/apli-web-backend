using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingGain.Api.Domain.Models;
using TrainingGain.Api.Domain.Repositories;
using TrainingGain.Api.Domain.Services;
using TrainingGain.Api.Domain.Services.Communication;

namespace TrainingGain.Api.Services
{
    public class TagSessionService: ITagSessionService
    {
        private readonly ITagSessionRepository _tagSessionRepository;   
        private readonly IUnitOfWork _unitOfWork;

        public TagSessionService(ITagSessionRepository tagSessionRepository, IUnitOfWork unitOfWork)
        {
            _tagSessionRepository = tagSessionRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<TagSessionResponse> AssignTagSessionAsync(int tagId, int sessionId)
        {
            try
            {
                await _tagSessionRepository.AssignTagSessionAsync(tagId, sessionId);
                await _unitOfWork.CompleteAsync();
                TagSession tagSession = await _tagSessionRepository.FindByTagIdAndSessionId(tagId, sessionId);
                return new TagSessionResponse(tagSession);
            }
            catch (Exception ex)
            {
                return new TagSessionResponse($"An error ocurred while assigning tag to session:{ex.Message}");
            }
        }

        public async Task<IEnumerable<TagSession>> ListAsync()
        {
            return await _tagSessionRepository.ListAsync();
        }

        public async Task<IEnumerable<TagSession>> ListBySessionIdAsync(int sessionId)
        {
            return await _tagSessionRepository.ListBySessionIdAsync(sessionId);
        }

        public async Task<IEnumerable<TagSession>> ListByTagIdAsync(int tagId)
        {
            return await _tagSessionRepository.ListByTagIdAsync(tagId);
        }
    }
}
