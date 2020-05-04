namespace HanuEdmsApi.ViewModels
{
    public class Grades
    {
        public string CourseCode { get; set; }
        public string CourseName { get; set; }
        public int CreditCount { get; set; }
        public double Attendance { get; set; }
        public double Midterm { get; set; }
        public double Exam { get; set; }
        public double Aggregate { get; set; }
        public int SemesterId { get; set; }
    }
}