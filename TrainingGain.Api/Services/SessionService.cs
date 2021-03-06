﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingGain.Api.Domain.Models;
using TrainingGain.Api.Domain.Repositories;
using TrainingGain.Api.Domain.Services;
using TrainingGain.Api.Domain.Services.Communication;

namespace TrainingGain.Api.Services
{
    public class SessionService : ISessionService
    {
        private readonly ISessionRepository _sessionRepository;
        private readonly IHistoryRepository _historyRepository;
        private readonly IEquipamentSessionRepository _equipamentSessionRepository;
        private readonly ITagSessionRepository _tagSessionRepository;
        public readonly IUnitOfWork _unitOfWork;

        public SessionService(ISessionRepository sessionRepository, IUnitOfWork unitOfWork, IHistoryRepository historyRepository, IEquipamentSessionRepository equipamentSessionRepository, ITagSessionRepository tagSessionRepository)
        {

            _sessionRepository = sessionRepository;
            _unitOfWork = unitOfWork;
            _historyRepository = historyRepository;
            _equipamentSessionRepository = equipamentSessionRepository;
            _tagSessionRepository = tagSessionRepository;
        }

        public async Task<IEnumerable<Session>> ListAsync()
        {
            return await _sessionRepository.ListAsync();
        }

        public async Task<IEnumerable<Session>> ListAsyncByTittle(string tittle)
        {
            return await _sessionRepository.ListAsyncByTittle(tittle);
        }


        public async Task<SessionResponse> SaveAsync(Session session)
        {
           
            try
            {
                await _sessionRepository.AddAsync(session);
                await _unitOfWork.CompleteAsync();

                return new SessionResponse(session);
            }
            catch (Exception ex)
            {
                return new SessionResponse($"An error ocurred while saving session: {ex.Message}");
            }

        }

        public async Task<SessionResponse> UpdateAsync(int id, Session session)
        {
            var existingSession = await _sessionRepository.FindById(id);
            if (existingSession == null)
                return new SessionResponse("Session not found");

            existingSession.Title = session.Title;
            existingSession.Description = session.Description;
            existingSession.StartDate = session.StartDate;
            existingSession.StartHour = session.StartHour;
            existingSession.EndHour = session.EndHour;


            try
            {
                _sessionRepository.Update(existingSession);
                await _unitOfWork.CompleteAsync();

                return new SessionResponse(existingSession);
            }
            catch (Exception ex)
            {
                return new SessionResponse($"An error ocurred while updating session: {ex.Message}");
            }
        }

        public async Task<SessionResponse> DeleteAsync(int id)
        {
            var existingSession = await _sessionRepository.FindById(id);

            if (existingSession == null)
                return new SessionResponse("Session not found");

            try
            {
                _sessionRepository.Remove(existingSession);
                await _unitOfWork.CompleteAsync();

                return new SessionResponse(existingSession);
            }
            catch (Exception ex)
            {
                return new SessionResponse($"An error ocurred while deleting session: {ex.Message}");
            }
        }

        public async Task<IEnumerable<Session>> ListBySpecialistIdAsync(int specialistId) 
        {
            return await _sessionRepository.ListAsyncBySpecialistId(specialistId);
        }
        public async Task<IEnumerable<Session>> ListByCustomerIdAsync(int customerId)
        {
            var histories = await _historyRepository.ListByCustomerIdAsync(customerId);
            var sessions = histories.Select(s => s.Session).ToList();
            return sessions;
        }

        public async Task<IEnumerable<Session>> ListByEquipamentIdAsync(int equipamentId)
        {
            var equipamentSessions = await _equipamentSessionRepository.ListByEquipamentIdAsync(equipamentId);
            var sessions = equipamentSessions.Select(s => s.Session).ToList();
            return sessions;
        }

        public async Task<IEnumerable<Session>> ListByTagIdAsync(int tagId)
        {
            var tagSessions  = await _tagSessionRepository.ListByTagIdAsync(tagId);
            var sessions = tagSessions.Select(s => s.Session).ToList();
            return sessions;
        }
    }
    
}
