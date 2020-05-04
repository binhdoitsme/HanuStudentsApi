using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HanuEdmsApi.Converter
{
    public class GradesConverter : OneWayConverter<Models.Grades, ViewModels.Grades>
    {
        public GradesConverter() : base(FromDatabase) { }

        private static ViewModels.Grades FromDatabase(Models.Grades grades)
        {
            var course = grades.Course;
            return new ViewModels.Grades()
            {
                SemesterId = course.SemesterId,
                CourseName = course.CourseName,
                CourseCode = course.CourseCode,
                CreditCount = course.CreditCount,
                Attendance = grades.AttendanceMark,
                Midterm = grades.InternalMark,
                Exam = grades.FinalMark,
                Aggregate = grades.OverallMark
            };
        }
    }
}
