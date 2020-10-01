using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingGain.Api.Domain.Models;

namespace TrainingGain.Api.Domain.Services.Communication
{
    public class SpecialistResponse : BaseResponse<Specialist>
    {
        public SpecialistResponse(Specialist resource) : base(resource)
        {
        }

        public SpecialistResponse(string message) : base(message)
        {
        }
    }
}
