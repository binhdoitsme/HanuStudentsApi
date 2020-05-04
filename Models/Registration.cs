using System;
using System.Collections.Generic;

namespace HanuEdmsApi.Models
{
    public partial class Registration
    {
        public Registration()
        {
            FeeLine = new HashSet<FeeLine>();
            Grades = new HashSet<Grades>();
        }

        public int Id { get; set; }
        public bool Status { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public int CourseClassId { get; set; }
        public int SemesterId { get; set; }

        public virtual Course Course { get; set; }
        public virtual CourseClass CourseClass { get; set; }
        public virtual Semester Semester { get; set; }
        public virtual Student Student { get; set; }
        public virtual ICollection<FeeLine> FeeLine { get; set; }
        public virtual ICollection<Grades> Grades { get; set; }
    }
}
