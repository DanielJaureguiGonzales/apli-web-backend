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
        [MaxLength(15)]
        public string Username { get; set; }
        [Required]
        [MaxLength(12)]
        public string Password { get; set; }
        [Required]
        [MaxLength(30)]
        public string EmailAddress { get; set; }
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        [Required]
        [MaxLength(30)]
        public string Lastname { get; set; }
        [Required]
        public int Age { get; set; }

        [Required]
        [MaxLength(20)]
        public string Gender { get; set; }
        [Required]
        [MaxLength(50)]
        public string Address { get; set; }
        [Required]
        public long Phone { get; set; }
        [Required]
        public int CountryId { get; set; }
        [Required]
        public int CityId { get; set; }

    }
}
