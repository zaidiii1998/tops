﻿using System;

namespace HUTOPS.Models
{
    public class Admin
    {
        public int Id { get; set; }
        public string Name { get; set;}
        public string Email { get; set;}
        public int? UserType { get; set; }
    }
}