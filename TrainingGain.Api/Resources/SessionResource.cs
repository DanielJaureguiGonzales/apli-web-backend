using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrainingGain.Api.Resources
{
    public class SessionResource
    {
        public int Id { get; set; }
        public int SpecialistId { get; set; }
        public string Tittle { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }

    }
}
