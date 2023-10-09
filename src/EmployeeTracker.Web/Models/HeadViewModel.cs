using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System;

namespace EmployeeTracker.Web.Models
{
    public class HeadViewModel
    {
        [HiddenInput]
        public Guid Id { get; set; }

        [HiddenInput]
        public bool IsSelected { get; set; }

        
        [Required]
        public string Name { get; set; }
    }
}
