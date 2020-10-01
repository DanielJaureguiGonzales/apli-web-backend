using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrainingGain.Api.Domain.Models
{
    public partial class UserCurrency
    {
        public int UserId { get; set; }
        public int CurrencyId { get; set; }
        public int Id { get; set; }

        public virtual Currency Currency { get; set; }
        public virtual User User { get; set; }
    }
}
