using System;
using System.Collections.Generic;

namespace HanuEdmsApi.Models
{
    public partial class UserAccount
    {
        public int AccountId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastLogin { get; set; }

        public virtual Student Student { get; set; }
    }
}
