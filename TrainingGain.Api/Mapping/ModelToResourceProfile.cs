using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingGain.Api.Domain.Models;
using TrainingGain.Api.Resources;

namespace TrainingGain.Api.Mapping
{
    public class ModelToResourceProfile: Profile
    {
        public ModelToResourceProfile()
        {
            CreateMap<User, UserResource>();
            CreateMap<Specialist, SpecialistResource>();
            CreateMap<Customer, CustomerResource>();
            CreateMap<Session, SessionResource>();
            CreateMap<SubscriptionPlan, SubscriptionPlanResource>();
            CreateMap<Subscription, SubscriptionResource>();
            CreateMap<History, HistoryResource>();
        }

    }
}
