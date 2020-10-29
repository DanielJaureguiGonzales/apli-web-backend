using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TrainingGain.Api.Resources
{
    public class SaveSubscriptionResource
    { 
        [Required]
        public int CustomerId { get; set; }
        [Required]
        public int SubscriptionPlanId { get; set; }
        [Required]
        public DateTime ExpiryDate { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
    }
}
