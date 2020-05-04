using System;
using System.Collections.Generic;

namespace HanuEdmsApi.Models
{
    public partial class Student
    {
        public Student()
        {
            Grades = new HashSet<Grades>();
            Registration = new HashSet<Registration>();
        }

        public int Id { get; set; }
        public string ClassName { get; set; }
        public int PassedCreditCount { get; set; }
        public double OverallMark { get; set; }
        public int? FacultyId { get; set; }
        public int? AcademicYearId { get; set; }

        public virtual AcademicYear AcademicYear { get; set; }
        public virtual Faculty Faculty { get; set; }
        public virtual Profile BasicProfile { get; set; }
        public virtual UserAccount UserAccount { get; set; }
        public virtual ICollection<Grades> Grades { get; set; }
        public virtual ICollection<Registration> Registration { get; set; }
    }
}
