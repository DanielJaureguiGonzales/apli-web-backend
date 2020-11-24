using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TrainingGain.Api.Resources
{
    public class SaveCustomerResource
    {
        [Required]
        [MaxLength(30)]
        public string Description { get; set; }
        [Required]
        public int UserId { get; set; }
    }
}
