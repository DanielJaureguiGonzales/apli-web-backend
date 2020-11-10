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
    public class HistoryService:IHistoryService
    {
        private readonly IHistoryRepository _historyRepository; 
        private readonly IUnitOfWork _unitOfWork;

        public HistoryService(IHistoryRepository historyRepository, IUnitOfWork unitOfWork)
        {
            _historyRepository = historyRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<HistoryResponse> AssignHistoryAsync(int customerId, int sessionId,DateTime Watched)   
        {
            try
            {
                await _historyRepository.AssingHistory(customerId, sessionId, Watched);
                await _unitOfWork.CompleteAsync();
                History history = await _historyRepository.FindByCustomerIdAndSessionId(customerId, sessionId);
                return new HistoryResponse(history);
            }
            catch (Exception ex)
            {
                return new HistoryResponse($"An error ocurred while assigning history to session:{ex.Message}");
            }
        }

        public async Task<IEnumerable<History>> ListAsync()
        {
            return await _historyRepository.ListAsync();
        }

        public async Task<IEnumerable<History>> ListByCustomerIdAsync(int customerId)
        {
            return await _historyRepository.ListByCustomerIdAsync(customerId);
        }

        public async Task<IEnumerable<History>> ListBySessionIdAsync(int sessionId)
        {
            return await _historyRepository.ListBySessionIdAsync(sessionId);
        }

    }
}
