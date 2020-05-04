using System;
using System.Collections.Generic;

namespace HanuEdmsApi.Models
{
    public partial class CourseType
    {
        public CourseType()
        {
            Course = new HashSet<Course>();
        }

        public int Id { get; set; }
        public string CourseTypeName { get; set; }
        public int PricePerCredit { get; set; }

        public virtual ICollection<Course> Course { get; set; }
    }
}
