using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TrainingGain.Api.Resources
{
    public class SaveSubscriptionPlanResource
    {
        [Required]
        [MaxLength(50)]
        public string Description { get; set; }
        [Required]
        public int Cost { get; set; }
    }
}
