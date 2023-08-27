using System.ComponentModel.DataAnnotations;
using TaskManagement.Models;
using Task = TaskManagement.Models.Task;

namespace TaskManagement.DTO
{
    public class EmployeeDTO
    {
        public int UserId { get; set; }
        public string Username { get; set; }           

        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

    }
}
