using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingGain.Api.Domain.Models;

namespace TrainingGain.Api.Domain.Services.Communication
{
    public class SubscriptionPlanResponse : BaseResponse<SubscriptionPlan>
    {
        public SubscriptionPlanResponse(SubscriptionPlan resource) : base(resource)
        {
        }

        public SubscriptionPlanResponse(string message) : base(message)
        {
        }
    }
}
