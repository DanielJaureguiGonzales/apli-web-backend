using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingGain.Api.Domain.Models;
using TrainingGain.Api.Resources;

namespace TrainingGain.Api.Mapping
{
    public class ResourceToModelProfile : Profile
    {
        public ResourceToModelProfile()
        {
            CreateMap<SaveUserResource, User>();
            CreateMap<SaveSpecialistResource, Specialist>();
            CreateMap<SaveSessionResource, Session>();
            CreateMap<SaveCustomerResource, Customer>();
            CreateMap<SaveSubscriptionPlanResource, SubscriptionPlan>();
            CreateMap<SaveSubscriptionResource, Subscription>();
            CreateMap<SaveHistoryResource, History>();
            CreateMap<SaveReviewResource, Review>();
        }
    }
}
