using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingGain.Api.Domain.Models;
using TrainingGain.Api.Domain.Repositories;
using TrainingGain.Api.Domain.Services;

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

        public async Task<IEnumerable<History>> ListAsyn()
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
