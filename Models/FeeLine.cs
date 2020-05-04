using System;
using System.Collections.Generic;

namespace HanuEdmsApi.Models
{
    public partial class FeeLine
    {
        public int Id { get; set; }
        public bool Status { get; set; }
        public float LineSum { get; set; }
        public int RegistrationId { get; set; }
        public int CourseId { get; set; }

        public virtual Course Course { get; set; }
        public virtual Registration Registration { get; set; }

        public void MarkAsPaid()
        {
            Status = true;
        }
    }
}
