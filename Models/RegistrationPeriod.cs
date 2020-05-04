using System;
using System.Collections.Generic;

namespace HanuEdmsApi.Models
{
    public partial class RegistrationPeriod
    {
        public int Id { get; set; }
        public DateTime PeriodStart { get; set; }
        public DateTime PeriodEnd { get; set; }
        public int SemesterId { get; set; }

        public virtual Semester Semester { get; set; }
    }
}
