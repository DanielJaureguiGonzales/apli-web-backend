using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrainingGain.Api.Domain.Models
{
    public  class Subscription
    {
        public int CustomerId { get; set; } 
        public int SubscriptionPlanId { get; set; } 
        public DateTime ExpiryDate { get; set; }
        public DateTime StartDate { get; set; } 
        public  SubscriptionPlan SubscriptionPlan { get; set; }  
        public  Customer Customer { get; set; }  
    }   
}
