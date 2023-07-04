using Fahrtberechnung.Attributes;
using Fahrtberechnung.DTOs;
using Fahrtberechnung.Models;
using System.Linq.Expressions;

namespace Fahrtberechnung.Interfaces
{
    public interface IUserService
    {
        ValueTask<UserViewDto> CreateAsync(UserCreationDto userCreationDTO);

        ValueTask<UserViewDto> UpdateAsync(string login, string password, User user);

        ValueTask<bool> DeleteAsync(Expression<Func<User, bool>> expression);

        ValueTask<IEnumerable<UserViewDto>> GetAllAsync(
             Expression<Func<User, bool>> expression = null);

        ValueTask<UserViewDto> GetAsync(Expression<Func<User, bool>> expression);


        ValueTask<bool> ChangePasswordAsync(string oldPassword, [UserPassword] string newPassword);
    }
}
