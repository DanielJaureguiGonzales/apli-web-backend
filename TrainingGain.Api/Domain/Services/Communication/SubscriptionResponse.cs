using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingGain.Api.Domain.Models;

namespace TrainingGain.Api.Domain.Services.Communication
{
    public class SubscriptionResponse : BaseResponse<Subscription>
    {
        public SubscriptionResponse(string message) : base(message)
        {
        }

        public SubscriptionResponse(Subscription resource) : base(resource)
        {
        }
    }
}
