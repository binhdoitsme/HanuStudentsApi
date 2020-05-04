namespace HanuEdmsApi.ViewModels
{
    public class TimetableUnit
    {
        public string ClassCode { get; set; }
        public string CourseName { get; set; }
        public string Venue { get; set; }
        public string Instructor { get; set; }
        public string TimeStart { get; set; }
        public string TimeEnd { get; set; }
        public string GmeetUrl { get; set; }
        public int DayOfWeek { get; set; }
    }

}
