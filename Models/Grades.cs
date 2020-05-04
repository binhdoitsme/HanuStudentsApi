using System;
using System.Collections.Generic;

namespace HanuEdmsApi.Models
{
    public partial class Grades
    {
        public int Id { get; set; }
        public double AttendanceMark { get; set; }
        public double InternalMark { get; set; }
        public double FinalMark { get; set; }
        public double OverallMark { get; set; }
        public int CourseId { get; set; }
        public int StudentId { get; set; }
        public int RegistrationId { get; set; }

        public virtual Course Course { get; set; }
        public virtual Registration Registration { get; set; }
        public virtual Student Student { get; set; }
    }
}
