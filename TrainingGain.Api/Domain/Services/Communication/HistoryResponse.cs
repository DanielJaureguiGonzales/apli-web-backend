using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingGain.Api.Domain.Models;

namespace TrainingGain.Api.Domain.Services.Communication
{
    public class HistoryResponse : BaseResponse<History>
    {
        public HistoryResponse(History resource) : base(resource)
        {
        }

        public HistoryResponse(string message) : base(message)
        {
        }
    }
}
