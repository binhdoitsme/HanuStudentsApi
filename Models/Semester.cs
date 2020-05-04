using System;
using System.Collections.Generic;

namespace HanuEdmsApi.Models
{
    public partial class Semester
    {
        public Semester()
        {
            Course = new HashSet<Course>();
            Registration = new HashSet<Registration>();
            RegistrationPeriod = new HashSet<RegistrationPeriod>();
        }

        public int Id { get; set; }
        public string SemesterName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public virtual ICollection<Course> Course { get; set; }
        public virtual ICollection<Registration> Registration { get; set; }
        public virtual ICollection<RegistrationPeriod> RegistrationPeriod { get; set; }
    }
}
