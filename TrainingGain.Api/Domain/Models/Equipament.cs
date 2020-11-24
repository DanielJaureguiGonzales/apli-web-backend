﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrainingGain.Api.Domain.Models
{
    public class Equipament 
    {
        public int Id { get; set; }
        public string Name { get; set; }    
        public string Description { get; set; }    

        public List<EquipamentSession> EquipamentSessions { get; set; }
    }
}
