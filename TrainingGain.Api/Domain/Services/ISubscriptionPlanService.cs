using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingGain.Api.Domain.Models;
using TrainingGain.Api.Domain.Services.Communication;
using TrainingGain.Api.Resources;

namespace TrainingGain.Api.Domain.Services
{
    public interface ISubscriptionPlanService
    {
        Task<IEnumerable<SubscriptionPlan>> ListAsync();
        Task<IEnumerable<SubscriptionPlan>> ListByCustomerIdAsync(int customerId);

        Task<SubscriptionPlanResponse> GetByIdAsync(int id);
        Task<SubscriptionPlanResponse> UpdateAsync(int id, SubscriptionPlan subscriptionPlan);
        Task<SubscriptionPlanResponse> SaveAsync(SubscriptionPlan subscriptionPlan);
        Task<SubscriptionPlanResponse> DeleteAsync(int id);
    }
}
