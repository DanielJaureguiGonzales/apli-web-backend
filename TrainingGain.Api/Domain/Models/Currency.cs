using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrainingGain.Api.Domain.Models
{
    public partial class Currency
    {
        public Currency()
        {
            Usercurrency = new HashSet<UserCurrency>();
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public int Value { get; set; }

        public virtual ICollection<UserCurrency> Usercurrency { get; set; }
    }
}
