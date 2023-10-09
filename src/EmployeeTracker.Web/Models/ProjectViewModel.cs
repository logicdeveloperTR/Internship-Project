using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System;

namespace EmployeeTracker.Web.Models
{
    public class ProjectViewModel
    {
        [HiddenInput]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}

