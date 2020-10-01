using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrainingGain.Api.Domain.Models
{
    public partial class Country
    {
        public Country()
        {
            City = new HashSet<City>();
            User = new HashSet<User>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public long Poblation { get; set; }
        public string Continent { get; set; }
        public double Density { get; set; }

        public virtual ICollection<City> City { get; set; }
        public virtual ICollection<User> User { get; set; }
    }
}
