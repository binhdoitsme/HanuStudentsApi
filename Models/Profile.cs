using System;
using System.Collections.Generic;

namespace HanuEdmsApi.Models
{
    public partial class Profile
    {
        public int Id { get; set; }
        public string DisplayName { get; set; }
        public DateTime Dob { get; set; }
        public sbyte Gender { get; set; }
        public string Nationality { get; set; }
        public string Hometown { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public virtual Student Student { get; set; }
    }
}
