using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrainingGain.Api.Domain.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
       
       
        public  User User { get; set; }

        public List<Subscription> Subscriptions { get; set; }
        public List<History> Histories { get; set; }    
        public List<Review> Reviews { get; set; }
    }
}
