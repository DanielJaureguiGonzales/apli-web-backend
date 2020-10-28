using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrainingGain.Api.Domain.Models
{
    public class Session
    {
        public int Id { get; set; }
        public int SpecialistId { get; set; }
        public string Tittle { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public Specialist Specialist { get; set; }  
        public List<History> Histories { get; set; }
    }
}
