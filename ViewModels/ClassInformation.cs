using System.Collections.Generic;

namespace HanuEdmsApi.ViewModels
{
    public class ClassInformation
    {
        public string ClassCode { get; set; }
        public string CourseName { get; set; }
        public int CreditCount { get; set; }
        public ICollection<TimetableUnit> Times { get; set; }
        public ICollection<Student> Students { get; set; }
    }
}