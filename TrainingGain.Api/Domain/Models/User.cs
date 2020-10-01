using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrainingGain.Api.Domain.Models
{
    public partial class User
    {
        public User()
        {
            Customer = new HashSet<Customer>();
            Session = new HashSet<Session>();
            Specialist = new HashSet<Specialist>();
            Subscriptiondetail = new HashSet<SubscriptionDetail>();
            Usercurrency = new HashSet<UserCurrency>();
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string EmailAddress { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public long Phone { get; set; }
        public int CountryId { get; set; }
        public int CityId { get; set; }

        public virtual City City { get; set; }
        public virtual Country Country { get; set; }
        public virtual ICollection<Customer> Customer { get; set; }
        public virtual ICollection<Session> Session { get; set; }
        public virtual ICollection<Specialist> Specialist { get; set; }
        public virtual ICollection<SubscriptionDetail> Subscriptiondetail { get; set; }
        public virtual ICollection<UserCurrency> Usercurrency { get; set; }
    }
}
