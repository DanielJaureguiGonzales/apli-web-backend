using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingGain.Api.Domain.Models;
using TrainingGain.Api.Domain.Persistance.Context;
using TrainingGain.Api.Domain.Repositories;

namespace TrainingGain.Api.Persistance.Repositories
{
    public class SubscriptionPlanRepository : BaseRepository, ISubscriptionPlanRepository
    {
        public SubscriptionPlanRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(SubscriptionPlan subscriptionPlan)   
        {
            await _context.SubscriptionPlans.AddAsync(subscriptionPlan);
        }

        public async Task<SubscriptionPlan> FindById(int subscriptionPlanId)
        {
           return await _context.SubscriptionPlans.FindAsync(subscriptionPlanId);
        }

        public async Task<IEnumerable<SubscriptionPlan>> ListAsync()
        {
            return await _context.SubscriptionPlans.ToListAsync();
        }

        public void Remove(SubscriptionPlan subscriptionPlan)
        {
            _context.SubscriptionPlans.Remove(subscriptionPlan);
        }

        public void Update(SubscriptionPlan subscriptionPlan)
        {
            _context.SubscriptionPlans.Update(subscriptionPlan);
        }
    }
}
