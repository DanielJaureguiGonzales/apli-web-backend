using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingGain.Api.Domain.Models;

namespace TrainingGain.Api.Domain.Services.Communication
{
    public class EquipamentResponse : BaseResponse<Equipament>
    {
        public EquipamentResponse(Equipament resource) : base(resource)
        {
        }

        public EquipamentResponse(string message) : base(message)
        {
        }
    }
}
