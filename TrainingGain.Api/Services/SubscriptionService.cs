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
    public class SubscriptionService : ISubscriptionService
    {
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly IUnitOfWork _unitOfWork;
        public SubscriptionService(ISubscriptionRepository subscriptionRepository, IUnitOfWork unitOfWork)
        {
            _subscriptionRepository = subscriptionRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<SubscriptionResponse> AssignSubscriptionAsync(int customerId, int subscriptionplanId)
        {
            try
            {
                await _subscriptionRepository.AssingSubscription(customerId, subscriptionplanId);
                await _unitOfWork.CompleteAsync();
                Subscription subscription = await _subscriptionRepository.FindByCustomerIdAndSubscriptionPlanIdId(customerId, subscriptionplanId);
                return new SubscriptionResponse(subscription);
            }
            catch (Exception ex)
            {
                return new SubscriptionResponse($"An error ocurred while assigning SubscriptionPlan to Customer:{ex.Message}");
            }
        }

        public async Task<SubscriptionResponse> UnassignSubscriptionAsync(int customerId, int subscriptionplanId)
        {
            try
            {
                _subscriptionRepository.UnassingSubscription(customerId, subscriptionplanId);
                await _unitOfWork.CompleteAsync();
                return new SubscriptionResponse(new Subscription { CustomerId = customerId, SubscriptionPlanId = subscriptionplanId });
            }
            catch (Exception ex)
            {
                return new SubscriptionResponse($"An error ocurred while unassigning SubscriptionPlan to Customer:{ex.Message}");

            }
        }

        public async Task<IEnumerable<Subscription>> ListAsyn()
        {
            return await _subscriptionRepository.ListAsync();
        }

        public async Task<IEnumerable<Subscription>> ListByCustomerIdAsync(int customerId)
        {
            return await _subscriptionRepository.ListByCustomerIdAsync(customerId);
        }

        public async Task<IEnumerable<Subscription>> ListBySubscriptionPlanIdAsync(int subscriptionplanId)
        {
            return await _subscriptionRepository.ListBySubscriptionPlanIdAsync(subscriptionplanId);
        }

       
    }
}
