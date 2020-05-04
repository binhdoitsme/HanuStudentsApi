using System;
using System.Collections.Generic;

namespace HanuEdmsApi.Models
{
    public partial class Article
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
