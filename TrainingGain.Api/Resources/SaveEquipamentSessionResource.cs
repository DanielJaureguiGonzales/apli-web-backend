using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TrainingGain.Api.Resources
{
    public class SaveEquipamentSessionResource
    {
        [Required]
        public int EquipamentId { get; set; }
        [Required]
        public int SessionId { get; set; }
    }
}
