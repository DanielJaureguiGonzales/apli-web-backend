using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingGain.Api.Domain.Models;

namespace TrainingGain.Api.Domain.Services.Communication
{
    public class TagSessionResponse : BaseResponse<TagSession>
    {
        public TagSessionResponse(TagSession resource) : base(resource)
        {
        }

        public TagSessionResponse(string message) : base(message)
        {
        }
    }
}
