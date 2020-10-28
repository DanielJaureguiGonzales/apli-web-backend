using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TrainingGain.Api.Resources
{
    public class SaveSessionResource
    {
        [Required]
        [MaxLength(30)]
        public string Tittle { get; set; }
        [Required]
        [MaxLength(50)]
        public string Description { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public int SpecialistId { get; set; }

    }
}
