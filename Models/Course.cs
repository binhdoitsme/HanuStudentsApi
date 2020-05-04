using System;
using System.Collections.Generic;

namespace HanuEdmsApi.Models
{
    public partial class Course
    {
        public Course()
        {
            CourseClass = new HashSet<CourseClass>();
            FeeLine = new HashSet<FeeLine>();
            Grades = new HashSet<Grades>();
            Registration = new HashSet<Registration>();
        }

        public int Id { get; set; }
        public string CourseCode { get; set; }
        public string CourseName { get; set; }
        public int CreditCount { get; set; }
        public int DifficultyLevel { get; set; }
        public bool Required { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int CourseTypeId { get; set; }
        public int FacultyId { get; set; }
        public int SemesterId { get; set; }

        public virtual CourseType CourseType { get; set; }
        public virtual Faculty Faculty { get; set; }
        public virtual Semester Semester { get; set; }
        public virtual ICollection<CourseClass> CourseClass { get; set; }
        public virtual ICollection<FeeLine> FeeLine { get; set; }
        public virtual ICollection<Grades> Grades { get; set; }
        public virtual ICollection<Registration> Registration { get; set; }
    }
}
