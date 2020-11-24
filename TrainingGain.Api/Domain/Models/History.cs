using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrainingGain.Api.Domain.Models
{
    public class History
    {
        public int CustomerId { get; set; } 
        public int SessionId { get; set; }

        public DateTime Watched { get; set; }

        public Customer Customer { get; set; }

        public Session Session { get; set; }    

    }
}
