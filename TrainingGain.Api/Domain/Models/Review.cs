using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrainingGain.Api.Domain.Models
{
    public class Review
    {
        public string Description { get; set; }

        public int Rank { get; set; }

        public int CustomerId { get; set; } 

        public int SpecialistId { get; set; }

        public Customer Customer { get; set; }

        public Specialist Specialist { get; set; }

    }
}
