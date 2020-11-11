using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TrainingGain.Api.Resources
{
    public class SaveTagSessionResource
    {   
        [Required]
        public int TagId { get; set; }
        [Required]
        public int SessionId { get; set; }
    }
}
