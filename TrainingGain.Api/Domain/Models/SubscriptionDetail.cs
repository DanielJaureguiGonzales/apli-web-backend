using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrainingGain.Api.Domain.Models
{
    public partial class SubscriptionDetail
    {
        public int UserId { get; set; }
        public int SubscriptionId { get; set; }
        public DateTime ExpiryDate { get; set; }
        public DateTime IssueDate { get; set; }
        public int Id { get; set; }

        public virtual Subscription Subscription { get; set; }
        public virtual User User { get; set; }
    }
}
