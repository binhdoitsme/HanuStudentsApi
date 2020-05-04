using HanuEdmsApi.Models;
using HanuEdmsApi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HanuEdmsApi.Converter
{
    public class RegistrationClassConverter : OneWayConverter<CourseClass, RegistrationClass>
    {
        public RegistrationClassConverter() : base(FromDatabase) { }

        private static RegistrationClass FromDatabase(CourseClass courseClass)
        {
            TimetableUnitConverter timetableConverter = new TimetableUnitConverter();

            return new RegistrationClass()
            {
                ClassCode = courseClass.CourseClassCode,
                CourseCode = courseClass.Course.CourseCode,
                CourseName = courseClass.Course.CourseName,
                Instructor = string.Join(", ", courseClass.Timetable.Select(t => t.InstructorName)
                                                .Where(s => s != null && s != string.Empty).ToList()),
                RemainingSlots = courseClass.RemainingSlots,
                MaxSlots = courseClass.MaxSlots,
                Semester = courseClass.Course.SemesterId,
                Timetables = courseClass.Timetable.Select(t => timetableConverter.ForwardConverter(t)).ToList()
            };
        }
    }
}
