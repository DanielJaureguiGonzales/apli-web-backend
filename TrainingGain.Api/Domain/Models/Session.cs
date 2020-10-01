using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrainingGain.Api.Domain.Models
{
    public partial class Session
    {
        public int Id { get; set; }
        public string Tittle { get; set; }
        public string Description { get; set; }
        public TimeSpan Duration { get; set; }
        public DateTime StartDate { get; set; }
        public bool Enable { get; set; }
        public double Cost { get; set; }
        public int UserId { get; set; }

        public virtual User User { get; set; }
    }
}
