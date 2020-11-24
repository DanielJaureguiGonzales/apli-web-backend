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
    public class SubscriptionPlanService : ISubscriptionPlanService
    {
        private readonly ISubscriptionPlanRepository _subscriptionPlanRepository;
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SubscriptionPlanService(ISubscriptionPlanRepository subscriptionPlanRepository, IUnitOfWork unitOfWork, ISubscriptionRepository subscriptionRepository)
        {
            _subscriptionPlanRepository = subscriptionPlanRepository;
            _unitOfWork = unitOfWork;
            _subscriptionRepository = subscriptionRepository;
        }

        public async Task<SubscriptionPlanResponse> DeleteAsync(int id)
        {
            var existingSubscriptionPlan = await _subscriptionPlanRepository.FindById(id);

            if(existingSubscriptionPlan==null)
            {
                return new SubscriptionPlanResponse("subscription plan not found");
            }

            try
            {
                _subscriptionPlanRepository.Remove(existingSubscriptionPlan);
                await _unitOfWork.CompleteAsync();

                return new SubscriptionPlanResponse(existingSubscriptionPlan);
            }
            catch (Exception ex)
            {
                return new SubscriptionPlanResponse($"An error ocurred while deleting subscription plan: {ex.Message}");
            }
        }

        public async Task<SubscriptionPlanResponse> GetByIdAsync(int id)
        {
            var existingSubscriptionPlan = await _subscriptionPlanRepository.FindById(id);
            if (existingSubscriptionPlan == null)
            {
                return new SubscriptionPlanResponse("subscription plan not found");
            }
            return new SubscriptionPlanResponse(existingSubscriptionPlan);
        }

        public async Task<IEnumerable<SubscriptionPlan>> ListAsync()
        {
            return await _subscriptionPlanRepository.ListAsync();
        }

        public async Task<IEnumerable<SubscriptionPlan>> ListByCustomerIdAsync(int customerId)
        {
            var subscriptions = await _subscriptionRepository.ListByCustomerIdAsync(customerId);
            var subscriptionPlans = subscriptions.Select(s => s.SubscriptionPlan).ToList();
            return subscriptionPlans;
        }

        public async Task<SubscriptionPlanResponse> SaveAsync(SubscriptionPlan subscriptionPlan)
        {
            try
            {
                await _subscriptionPlanRepository.AddAsync(subscriptionPlan);
                await _unitOfWork.CompleteAsync();

                return new SubscriptionPlanResponse(subscriptionPlan);
            }
            catch (Exception ex)
            {
                return new SubscriptionPlanResponse($"An error ocurred while saving the subscription plan: {ex.Message}");
            }
        }

        public async Task<SubscriptionPlanResponse> UpdateAsync(int id, SubscriptionPlan subscriptionPlan)
        {
            var existingSubscriptionPlan = await _subscriptionPlanRepository.FindById(id);

            if (existingSubscriptionPlan == null)
            {
                return new SubscriptionPlanResponse("subscription plan not found");
            }


            existingSubscriptionPlan.Name = subscriptionPlan.Name;
            existingSubscriptionPlan.Description = subscriptionPlan.Description;
            existingSubscriptionPlan.Cost = subscriptionPlan.Cost;

            try
            {
                _subscriptionPlanRepository.Update(existingSubscriptionPlan);
                await _unitOfWork.CompleteAsync();

                return new SubscriptionPlanResponse(existingSubscriptionPlan);
            }
            catch (Exception ex)
            {
                return new SubscriptionPlanResponse($"An error ocurred while updating subscription Plan: {ex.Message}");
            }
        }
    }
}
