﻿using System;
using System.ComponentModel.DataAnnotations;

namespace API.DTOs.Users
{
    public class UpdateUserRequest
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Address { get; set; }
        [Required]
        [StringLength(125)]
        public string Password { get; set; }

        [StringLength(255)]
        public string Email { get; set; }

        [Required]
        public int CompanyId { get; set; }
    }
}