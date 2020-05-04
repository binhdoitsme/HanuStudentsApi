using HanuEdmsApi.Models;
using HanuEdmsApi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HanuEdmsApi.Converter
{
    public class ClassInformationConverter : OneWayConverter<CourseClass, ClassInformation>
    {
        public ClassInformationConverter() : base(FromDatabase) { }

        private static ClassInformation FromDatabase(CourseClass courseClass)
        {
            ProfileConverter profileConverter = new ProfileConverter();
            TimetableUnitConverter timetableConverter = new TimetableUnitConverter();

            return new ClassInformation()
            {
                ClassCode = courseClass.CourseClassCode,
                CourseName = courseClass.Course.CourseName,
                CreditCount = courseClass.Course.CreditCount,
                Students = courseClass.Registration.Select(r => 
                            new ViewModels.Student(profileConverter.ForwardConverter(r.Student))).ToList(),
                Times = courseClass.Timetable.Select(t => timetableConverter.ForwardConverter(t)).ToList()
            };
        }
    }
}
