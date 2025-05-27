using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementSystem.Models
{
    public class Position
    {
        public int PositionId { get; set; }

        [Required(ErrorMessage = "Pozisyon adı zorunludur.")]
        [Display(Name = "Pozisyon Adı")]
        [StringLength(100, ErrorMessage = "Pozisyon adı en fazla 100 karakter olabilir.")]
        public string Name { get; set; }

        public ICollection<Employee> Employees { get; set; }
    }
} 