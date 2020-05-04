using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HanuEdmsApi.ViewModels
{
    public class RegistrationClass
    {
        public string CourseCode { get; set; }
        public string CourseName { get; set; }
        public string ClassCode { get; set; }
        public string Instructor { get; set; }
        public int RemainingSlots { get; set; }
        public int MaxSlots { get; set; }
        public int Semester { get; set; }
        public ICollection<TimetableUnit> Timetables { get; set; }
    }
}
