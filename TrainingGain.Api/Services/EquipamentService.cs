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
    public class EquipamentService : IEquipamentService
    {
        private readonly IEquipamentRepository _equipamentRepository;
        private readonly IEquipamentSessionRepository _equipamentSessionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public EquipamentService(IEquipamentRepository equipamentRepository, IEquipamentSessionRepository equipamentSessionRepository, IUnitOfWork unitOfWork)
        {
            _equipamentRepository = equipamentRepository;
            _equipamentSessionRepository = equipamentSessionRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<EquipamentResponse> DeleteAsync(int id)
        {
            var existingEquipament = await _equipamentRepository.FindById(id);

            if (existingEquipament == null)
            {
                return new EquipamentResponse("Equipament not found");
            }

            try
            {
                _equipamentRepository.Remove(existingEquipament);
                await _unitOfWork.CompleteAsync();

                return new EquipamentResponse(existingEquipament);
            }
            catch (Exception ex)
            {
                return new EquipamentResponse($"An error ocurred while deleting equipament: {ex.Message}");
            }
        }

        public async Task<EquipamentResponse> GetByIdAsync(int id)
        {
            var existingEquipament = await _equipamentRepository.FindById(id);
            if (existingEquipament == null)
            {
                return new EquipamentResponse("Equipament not found");
            }
            return new EquipamentResponse(existingEquipament);
        }

        public async Task<IEnumerable<Equipament>> ListAsync()
        {
            return await _equipamentRepository.ListAsync();
        }

        public async Task<IEnumerable<Equipament>> ListBySessionIdAsync(int sessionId)
        {
            var equipamentSessions = await _equipamentSessionRepository.ListBySessionIdAsync(sessionId);
            var equipaments = equipamentSessions.Select(s => s.Equipament).ToList();
            return equipaments;
        }

        public async Task<EquipamentResponse> SaveAsync(Equipament equipament)
        {
            try
            {
                await _equipamentRepository.AddAsync(equipament);
                await _unitOfWork.CompleteAsync();

                return new EquipamentResponse(equipament);
            }
            catch (Exception ex)
            {
                return new EquipamentResponse($"An error ocurred while saving the equipament: {ex.Message}");
            }
        }

        public async Task<EquipamentResponse> UpdateAsync(int id, Equipament equipament)
        {
            var equipamentSessions = await _equipamentRepository.FindById(id);

            if (equipamentSessions == null)
            {
                return new EquipamentResponse("Equipament not found");
            }

            equipamentSessions.Description = equipament.Description;
            equipamentSessions.Name = equipament.Name;

            try
            {
                _equipamentRepository.Update(equipamentSessions);
                await _unitOfWork.CompleteAsync();

                return new EquipamentResponse(equipamentSessions);
            }
            catch (Exception ex)
            {
                return new EquipamentResponse($"An error ocurred while updating equipament: {ex.Message}");
            }
        }
    }
}
