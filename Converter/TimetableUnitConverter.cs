using HanuEdmsApi.Models;
using System.Collections.Generic;

namespace HanuEdmsApi.Converter
{
    public class TimetableUnitConverter : OneWayConverter<Models.Timetable, ViewModels.TimetableUnit>
    {
        public TimetableUnitConverter() : base(FromDatabase) { }

        private static readonly IDictionary<int, string[]> SESSION_MAP = new Dictionary<int, string[]>
        {
            { 1, new string[] { "07:20", "09:00" } },
            { 2, new string[] { "09:30", "11:10" } },
            { 3, new string[] { "12:20", "14:00" } },
            { 4, new string[] { "14:30", "16:10" } },
            { 5, new string[] { "17:00", "19:20" } }
        };

        private static ViewModels.TimetableUnit FromDatabase(Models.Timetable timetable)
        {
            if (timetable is null) return null;
            CourseClass @class = timetable.CourseClass;

            return new ViewModels.TimetableUnit()
            {
                ClassCode = @class.CourseClassCode,
                CourseName = @class.Course.CourseName,
                DayOfWeek = timetable.DayOfWeek,
                GmeetUrl = "https://meet.google.com",
                Instructor = timetable.InstructorName,
                TimeStart = SESSION_MAP[timetable.SessionNo][0],
                TimeEnd = SESSION_MAP[timetable.SessionNo][1],
                Venue = timetable.Venue == null ? "" : timetable.Venue
            };
        }
    }
}
