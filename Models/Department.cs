using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementSystem.Models
{
    public class Department
    {
        public int DepartmentId { get; set; }

        [Required(ErrorMessage = "Department Name is required.")]
        [Display(Name = "Department Name")]
        public string Name { get; set; }
    }
}