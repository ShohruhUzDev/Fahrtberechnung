using System.ComponentModel.DataAnnotations;

namespace Fahrtberechnung.DTOs
{
    public class UserUpdatDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }
    }
}
