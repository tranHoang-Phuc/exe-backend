using System.ComponentModel.DataAnnotations;

namespace TuiToot.Server.Api.Dtos.Request
{
    public class RegistrationRequest
    {
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        [StringLength(10, ErrorMessage = "Phone number must not exceed 10 characters.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
        [MaxLength(32, ErrorMessage = "Password must not exceed 32 characters.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        public string Address { get; set; }
    }
}
