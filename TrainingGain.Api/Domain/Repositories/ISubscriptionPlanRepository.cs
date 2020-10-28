using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingGain.Api.Domain.Models;

namespace TrainingGain.Api.Domain.Repositories
{
    public interface ISubscriptionPlanRepository
    {
        Task<IEnumerable<SubscriptionPlan>> ListAsync();
        Task<SubscriptionPlan> FindById(int subscriptionPlanId);  
        Task AddAsync(SubscriptionPlan subscriptionPlan);   
        void Update(SubscriptionPlan subscriptionPlan);
        void Remove(SubscriptionPlan subscriptionPlan); 
    }
}
