using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrainingGain.Api.Domain.Services.Communication
{
    public abstract class BaseResponse<T>
    {
        protected BaseResponse(T resource)
        {
            Resource = resource;
            Success = true;
            Message = string.Empty;
        }

        protected BaseResponse(string message)
        {
            Message = message;
            Success = false;
        }

        public bool Success { get; protected set; }
        public string Message { get; protected set; }
        public T Resource { get; protected set; }

    }
}
