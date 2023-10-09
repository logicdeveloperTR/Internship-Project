using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace EmployeeTracker.Web.Models
{
    public class LeaderViewModel
    {
        [HiddenInput]
        public Guid Id { get; set; }

        public bool IsSelected { get; set; }

        [Required]
        [HiddenInput]
        public string Name { get; set; }    
    }
}
