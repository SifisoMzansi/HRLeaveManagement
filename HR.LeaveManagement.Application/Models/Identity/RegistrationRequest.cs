using System.ComponentModel.DataAnnotations;

namespace HR.LeaveManagement.Application.Models.Identity
{
    public class RegistrationRequest
    {
        [Required]
        public string FirstName { set; get; } = null!;
        [Required]
        public string LastName { set; get; } = null!;
        [Required]
        public string Email { set; get; } = null!;
        [Required]
        public string Password { set; get; } = null!;
        [Required]
        public string UserName { get; set; } = null!; 

    }
}
