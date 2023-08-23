using System.ComponentModel.DataAnnotations;
using TaskManagement.Models;

namespace TaskManagement.DTO
{
    public class ManagerDTO
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}
