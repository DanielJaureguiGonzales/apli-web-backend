using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrainingGain.Api.Resources
{
    public class HistoryResource
    {
        public int CustomerId { get; set; }
        public int SessionId { get; set; }

        public DateTime Watched { get; set; }
    }
}
