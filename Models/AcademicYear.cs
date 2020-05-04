using System;
using System.Collections.Generic;

namespace HanuEdmsApi.Models
{
    public partial class AcademicYear
    {
        public AcademicYear()
        {
            Student = new HashSet<Student>();
        }

        public int Id { get; set; }
        public string AcademicYearShort { get; set; }
        public string AcademicYearDescription { get; set; }
        public DateTime StartDate { get; set; }

        public virtual ICollection<Student> Student { get; set; }
    }
}
