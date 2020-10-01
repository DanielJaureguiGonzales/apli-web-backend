using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrainingGain.Api.Domain.Models
{
    public partial class Subscription
    {
        public Subscription()
        {
            Subscriptiondetail = new HashSet<SubscriptionDetail>();
        }

        public int Id { get; set; }
        public string Description { get; set; }

        public virtual ICollection<SubscriptionDetail> Subscriptiondetail { get; set; }
    }
}
