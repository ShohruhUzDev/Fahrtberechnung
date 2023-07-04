using System.ComponentModel.DataAnnotations;

namespace Fahrtberechnung.Attributes
{
    public class UserLogin :ValidationAttribute
    {
        public override bool IsValid(object value)
         => value is string login &&
             login.All(c => char.IsDigit(c) ||
             char.IsLetter(c) || c == '_') &&
             login.Any(c => char.IsLetter(c));
    }
}
