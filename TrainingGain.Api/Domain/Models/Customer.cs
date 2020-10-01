using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrainingGain.Api.Domain.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Userspecialist = new HashSet<UserSpecialist>();
        }

        public int UserId { get; set; }
        public string Description { get; set; }
        public int Id { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<UserSpecialist> Userspecialist { get; set; }
    }
}
