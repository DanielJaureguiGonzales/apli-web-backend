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
    public class EquipamentSessionService: IEquipamentSessionService
    {
        private readonly IEquipamentSessionRepository _equipamentSessionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public EquipamentSessionService(IEquipamentSessionRepository equipamentSessionRepository, IUnitOfWork unitOfWork)
        {
            _equipamentSessionRepository = equipamentSessionRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<EquipamentSessionResponse> AssignEquipamentSessionAsync(int equipamentId, int sessionId)
        {
            try
            {
                await _equipamentSessionRepository.AssignEquipamentSessionAsync(equipamentId, sessionId);
                await _unitOfWork.CompleteAsync();
                EquipamentSession equipamentSession = await _equipamentSessionRepository.FindByEquipamentIdAndSessionId(equipamentId, sessionId);
                return new EquipamentSessionResponse(equipamentSession);
            }
            catch (Exception ex)
            {
                return new EquipamentSessionResponse($"An error ocurred while assigning Equipament to Session:{ex.Message}");
            }
        }

        public async Task<IEnumerable<EquipamentSession>> ListAsync()
        {
            return await _equipamentSessionRepository.ListAsync();
        }

        public async Task<IEnumerable<EquipamentSession>> ListByEquipamentIdAsync(int equipamentId)
        {
            return await _equipamentSessionRepository.ListByEquipamentIdAsync(equipamentId);
        }

        public async Task<IEnumerable<EquipamentSession>> ListBySessionIdAsync(int sessionId)
        {
            return await _equipamentSessionRepository.ListBySessionIdAsync(sessionId);
        }

        public async Task<EquipamentSessionResponse> UnassignEquipamentSessionAsync(int equipamentId, int sessionId)
        {
            try
            {
                _equipamentSessionRepository.UnassignEquipamentSessionAsync(equipamentId, sessionId);
                await _unitOfWork.CompleteAsync();
                return new EquipamentSessionResponse(new EquipamentSession { EquipamentId = equipamentId, SessionId = sessionId });
            }
            catch (Exception ex)
            {
                return new EquipamentSessionResponse($"An error ocurred while unassigning Equipament to Session:{ex.Message}");

            }
        }
    }
}
