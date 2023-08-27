using System.ComponentModel.DataAnnotations;

namespace TaskManagement.DTO
{
    public class LoginViewModel
    {        
        [Required(ErrorMessage = "Please Enter Username")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Please Enter Password")]
        public string Password { get; set; }
    }
}
