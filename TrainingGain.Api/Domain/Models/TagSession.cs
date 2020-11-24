using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrainingGain.Api.Domain.Models
{
    public class TagSession
    {
        public int TagId { get; set; }  
        public int SessionId { get; set; }
        public Tag Tag { get; set; }
        public Session Session { get; set; }
    }
}
