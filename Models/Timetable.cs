using System;
using System.Collections.Generic;

namespace HanuEdmsApi.Models
{
    public partial class Timetable
    {
        public int Id { get; set; }
        public int DayOfWeek { get; set; }
        public int SessionNo { get; set; }
        public string Venue { get; set; }
        public string InstructorName { get; set; }
        public ulong IsLecture { get; set; }
        public int CourseClassId { get; set; }

        public virtual CourseClass CourseClass { get; set; }
    }
}
