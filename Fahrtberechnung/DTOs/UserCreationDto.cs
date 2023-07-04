using Fahrtberechnung.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Fahrtberechnung.DTOs
{
    public class UserCreationDto
    {
        [Required]
        public string FirstName { get; set; }
        [Required]

        public string LastName { get; set; }
        [Required]

        public string Username { get; set; }
        [Required]

        public string Password { get; set; }
        [Required]

        public string Email { get; set; }
    }
}
