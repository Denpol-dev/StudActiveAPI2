﻿using System;

namespace StudActiveAPI.Models
{
    public partial class Group
    {
        public Guid GroupId { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        public int CourseNumber { get; set; }
    }
}
