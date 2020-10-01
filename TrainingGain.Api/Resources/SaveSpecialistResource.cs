using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TrainingGain.Api.Resources
{
    public class SaveSpecialistResource
    {
        [Required]
        [MaxLength(20)]
        public string Specialty { get; set; }
    }
}
