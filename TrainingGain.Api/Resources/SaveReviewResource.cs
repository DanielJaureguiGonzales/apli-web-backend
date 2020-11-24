using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TrainingGain.Api.Resources
{
    public class SaveReviewResource
    {
        [Required]
        public string Description { get; set; }
        [Required]
        public int Rank { get; set; }
        [Required]
        public int CustomerId { get; set; }
        [Required]
        public int SpecialistId { get; set; }

    }
}
