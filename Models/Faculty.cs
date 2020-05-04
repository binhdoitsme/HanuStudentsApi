using System;
using System.Collections.Generic;

namespace HanuEdmsApi.Models
{
    public partial class Faculty
    {
        public Faculty()
        {
            Course = new HashSet<Course>();
            Student = new HashSet<Student>();
        }

        public int Id { get; set; }
        public string FacultyName { get; set; }
        public int RequiredCreditCount { get; set; }

        public virtual ICollection<Course> Course { get; set; }
        public virtual ICollection<Student> Student { get; set; }
    }
}
