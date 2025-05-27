using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementSystem.Models
{
    public class User
    {
        public int UserId { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
} 