using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrainingGain.Api.Resources
{
    public class ReviewResource
    {
        public string Description { get; set; }

        public int Rank { get; set; }

        public int CustomerId { get; set; }

        public int SpecialistId { get; set; }

    }
}
