using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrainingGain.Api.Domain.Models
{
    public partial class Specialist
    {
        public Specialist()
        {
            Userspecialist = new HashSet<UserSpecialist>();
        }

        public int UserId { get; set; }
        public string Specialty { get; set; }
        public int Id { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<UserSpecialist> Userspecialist { get; set; }
    }
}
