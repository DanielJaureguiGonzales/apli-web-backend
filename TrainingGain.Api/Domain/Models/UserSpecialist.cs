using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrainingGain.Api.Domain.Models
{
    public partial class UserSpecialist
    {
        public int CustomerId { get; set; }
        public int SpecialistId { get; set; }
        public int Userspecialist1 { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Specialist Specialist { get; set; }
    }
}
