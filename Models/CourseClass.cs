using System;
using System.Collections.Generic;

namespace HanuEdmsApi.Models
{
    public partial class CourseClass
    {
        public CourseClass()
        {
            Registration = new HashSet<Registration>();
            Timetable = new HashSet<Timetable>();
        }

        public int CourseClassId { get; set; }
        public string CourseClassCode { get; set; }
        public int RemainingSlots { get; set; }
        public int MaxSlots { get; set; }
        public int CourseId { get; set; }

        public virtual Course Course { get; set; }
        public virtual ICollection<Registration> Registration { get; set; }
        public virtual ICollection<Timetable> Timetable { get; set; }
    }
}
