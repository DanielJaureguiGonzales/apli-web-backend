using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrainingGain.Api.Domain.Models
{
    public class Specialist
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Specialty { get; set; }
       

        public User User { get; set; }

        public IList<Session> Sessions { get; set; } = new List<Session>();

        public List<Review> Reviews { get; set; }
    }
}
