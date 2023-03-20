﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

#nullable disable

namespace StudActiveAPI.Models
{
    public partial class User
    {
        public User()
        {
            Students = new HashSet<Student>();
        }

        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public Guid? Role { get; set; }
        public string Hash { get; set; }
        public byte[] Salt { get; set; }
        public bool isValid { get; set; }
        public int Permission { get; set; }

        public virtual ICollection<Student> Students { get; set; }
    }
}
