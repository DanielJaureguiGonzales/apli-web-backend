using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingGain.Api.Domain.Models;

namespace TrainingGain.Api.Domain.Services.Communication
{
    public class CustomerResponse : BaseResponse<Customer>
    {
        public CustomerResponse(Customer resource) : base(resource)
        {
        }

        public CustomerResponse(string message) : base(message)
        {
        }
    }
}
