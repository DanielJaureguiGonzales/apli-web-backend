using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TrainingGain.Api.Resources
{
    public class SaveUserResource
    {

        [Required]
        [MaxLength(10)]
        public string Name { get; set; }
        [Required]
        [MaxLength(15)]
        public string LastName { get; set; }
        [Required]
        public DateTime Birth { get; set; }
        [Required]
        [MaxLength(20)]
        public string Address { get; set; }
        [Required]
        public long Phone { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        [MaxLength(50)]
        public string Email { get; set; }
        [Required]
        [MaxLength(25)]
        public string Country { get; set; }
        [Required]
        [MaxLength(20)]
        public string Gender { get; set; }
        [Required]
        [MaxLength(8)]
        public string Password { get; set; }

    }
}
