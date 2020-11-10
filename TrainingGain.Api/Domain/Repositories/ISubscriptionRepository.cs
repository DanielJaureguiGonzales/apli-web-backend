using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingGain.Api.Domain.Models;

namespace TrainingGain.Api.Domain.Repositories
{
    public interface ISubscriptionRepository
    {
        Task<IEnumerable<Subscription>> ListAsync();
        Task<IEnumerable<Subscription>> ListByCustomerIdAsync(int customerId);  
        Task<IEnumerable<Subscription>> ListBySubscriptionPlanIdAsync(int subscriptionplanId);
        Task<Subscription> FindByCustomerIdAndSubscriptionPlanId(int customerId, int subscriptionplanId);    
        Task AddAsync(Subscription subscription);
        void Remove(Subscription subscription);
        Task AssingSubscription(int customerId, int subscriptionplanId, DateTime start_date, DateTime expiry_date);
        void UnassingSubscription(int customerId, int subscriptionplanId);        
    }
}
