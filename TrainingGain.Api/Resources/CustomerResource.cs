using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrainingGain.Api.Resources
{
    public class CustomerResource
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
    }
}
