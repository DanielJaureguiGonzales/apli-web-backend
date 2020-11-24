using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrainingGain.Api.Resources
{
    public class SpecialistResource 
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Specialty { get; set; }

    }
}
