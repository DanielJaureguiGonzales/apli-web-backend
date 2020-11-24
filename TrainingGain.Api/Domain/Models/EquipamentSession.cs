using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrainingGain.Api.Domain.Models
{
    public class EquipamentSession
    {
        public int EquipamentId { get; set; }   
        public int SessionId { get; set; }
        public Equipament Equipament { get; set; }
        public Session Session { get; set; } 

    }
}
