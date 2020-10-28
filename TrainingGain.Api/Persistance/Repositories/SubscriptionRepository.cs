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
    public class SubscriptionRepository : BaseRepository, ISubscriptionRepository
    {
        public SubscriptionRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(Subscription subscription)
        {
            await _context.Subscriptions.AddAsync(subscription);
        }

        public async Task AssingSubscription(int customerId, int subscriptionplanId)
        {
            Subscription subscription = await FindByCustomerIdAndSubscriptionPlanIdId(customerId, subscriptionplanId);
            if(subscription == null)
            {
                subscription = new Subscription { CustomerId = customerId, SubscriptionPlanId = subscriptionplanId };
                await AddAsync(subscription);
            }
        }

        public async Task<Subscription> FindByCustomerIdAndSubscriptionPlanIdId(int customerId, int subscriptionplanId)
        {
            return await _context.Subscriptions.FindAsync(customerId, subscriptionplanId);
        }

        public async Task<IEnumerable<Subscription>> ListAsync()
        {
            return await _context.Subscriptions.Include(s => s.Customer).Include(s => s.SubscriptionPlan).ToListAsync();
        }

        public async Task<IEnumerable<Subscription>> ListByCustomerIdAsync(int customerId)
        {
            return await _context.Subscriptions.Where(s => s.CustomerId == customerId).Include(S => S.Customer).Include(S => S.SubscriptionPlan).ToListAsync();
        }

        public async Task<IEnumerable<Subscription>> ListBySubscriptionPlanIdAsync(int subscriptionplanId)
        {
            return await _context.Subscriptions.Where(s => s.SubscriptionPlanId == subscriptionplanId).Include(S => S.Customer).Include(S => S.SubscriptionPlan).ToListAsync();

        }

        public void Remove(Subscription subscription)
        {
            _context.Subscriptions.Remove(subscription);
        }

        public async void UnassingSubscription(int customerId, int subscriptionplanId)  
        {
            Subscription subscription = await FindByCustomerIdAndSubscriptionPlanIdId(customerId, subscriptionplanId);
            if (subscription != null)
            {
                Remove(subscription);
            }
        }
    }
}
