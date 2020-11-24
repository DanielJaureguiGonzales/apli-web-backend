using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingGain.Api.Domain.Models;

namespace TrainingGain.Api.Domain.Services.Communication
{
    public class EquipamentSessionResponse : BaseResponse<EquipamentSession>
    {
        public EquipamentSessionResponse(EquipamentSession resource) : base(resource)
        {
        }

        public EquipamentSessionResponse(string message) : base(message)
        {
        }
    }
}
