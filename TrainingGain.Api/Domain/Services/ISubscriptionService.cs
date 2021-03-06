﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingGain.Api.Domain.Models;
using TrainingGain.Api.Domain.Services.Communication;

namespace TrainingGain.Api.Domain.Services
{
    public interface ISubscriptionService
    {
        Task<IEnumerable<Subscription>> ListAsync();
        Task<IEnumerable<Subscription>> ListByCustomerIdAsync(int customerId);
        Task<IEnumerable<Subscription>> ListBySubscriptionPlanIdAsync(int subscriptionplanId);
        Task<SubscriptionResponse> AssignSubscriptionAsync(int customerId, int subscriptionplanId, DateTime start_date, DateTime expiry_date);
        Task<SubscriptionResponse> UnassignSubscriptionAsync(int customerId, int subscriptionplanId);   
    }
}
