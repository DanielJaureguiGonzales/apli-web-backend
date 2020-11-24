using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TrainingGain.Api.Resources
{
    public class SaveHistoryResource
    {   
        [Required]
        public int CustomerId { get; set; }
        [Required]
        public int SessionId { get; set; }
        [Required]
        public DateTime Watched { get; set; }
    }
}
