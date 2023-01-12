﻿using System.ComponentModel.DataAnnotations;

namespace AASA_Back_End.Models
{
    public class Employee : BaseEntity
    {
        [Required]
        public string FullName { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        public string Position { get; set; }
        public bool IsActive { get; set; } = false;

    }
}
