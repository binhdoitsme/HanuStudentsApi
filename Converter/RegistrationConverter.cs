using HanuEdmsApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HanuEdmsApi.Converter
{
    public class RegistrationConverter : OneWayConverter<Registration, ViewModels.Registration>
    {
        public RegistrationConverter() : base(FromDatabase) { }

        private static ViewModels.Registration FromDatabase(Registration registration)
        {
            CourseClass courseClass = registration.CourseClass;
            Course course = registration.Course;
            return new ViewModels.Registration()
            {
                ClassCode = courseClass.CourseClassCode,
                CourseCode = course.CourseCode,
                Semester = course.SemesterId,
                StudentId = registration.StudentId
            };
        }
    }
}
