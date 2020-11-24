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
        public string Title { get; set; }   
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public string StartHour { get; set; }
        public string EndHour { get; set; }

    }
}
