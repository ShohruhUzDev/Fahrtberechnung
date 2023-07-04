using System.Linq.Expressions;
using Fahrtberechnung.Attributes;
using Fahrtberechnung.DTOs;
using Fahrtberechnung.Exceptions;
using Fahrtberechnung.Extensions;
using Fahrtberechnung.Helpers;
using Fahrtberechnung.Interfaces;
using Fahrtberechnung.IRepostories;
using Fahrtberechnung.Models;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Fahrtberechnung.Services
{
    public class UserService : IUserService
    {
        public IUnitOfWork unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async ValueTask<UserViewDto> CreateAsync(UserCreationDto userCreationDTO)
        {
            var alreadyExistUser = await unitOfWork.Users.GetAsync(u => u.Username == userCreationDTO.Username);

            if (alreadyExistUser != null)
                throw new FahrtberechnungException(400, "User With Such Username Already Exist");

            userCreationDTO.Password = userCreationDTO.Password.Encrypt();

            var user = await unitOfWork.Users.CreateAsync(userCreationDTO.Adapt<User>());
            user.CreatedAt = DateTime.UtcNow;
            await unitOfWork.SaveChangesAsync();
            return user.Adapt<UserViewDto>();

        }
        public async ValueTask<bool> ChangePasswordAsync(string oldPassword, [UserPassword] string newPassword)
        {

            var user = await unitOfWork.Users.GetAsync(u => u.Password == oldPassword.Encrypt());

            if (user == null)
                throw new FahrtberechnungException(404, "User not found");

            if (user.Password != oldPassword.Encrypt())
            {
                throw new FahrtberechnungException(400, "Password is Incorrect");
            }
            user.Password = newPassword.Encrypt();

            unitOfWork.Users.Update(user);
            await unitOfWork.SaveChangesAsync();
            return true;
        }


        public async ValueTask<bool> DeleteAsync(Expression<Func<User, bool>> expression)
        {
            var isDeleted = await unitOfWork.Users.DeleteAsync(expression);
            if (!isDeleted)
                throw new FahrtberechnungException(404, "User not found");
            return true;
        }

        public async ValueTask<IEnumerable<UserViewDto>> GetAllAsync(Expression<Func<User, bool>> expression = null)
        {
            var users = unitOfWork.Users.GetAll(expression: expression, null, false);

            return (await users.ToListAsync()).Adapt<List<UserViewDto>>();
        }

        public async ValueTask<UserViewDto> GetAsync(Expression<Func<User, bool>> expression)
        {
            var user = await unitOfWork.Users.GetAsync(expression);

            if (user is null)
                throw new FahrtberechnungException(404, "User not found");

            return user.Adapt<UserViewDto>();
        }

        public async ValueTask<UserViewDto> UpdateAsync(string login, string password, UserUpdatDto userForUpdateDto)
        {
            //var alreadyExistUser = await unitOfWork.Users.GetAsync(u => u.Username == userForUpdateDto.Username && u.Id != HttpContextHelper.UserId);

            //if (alreadyExistUser != null)
            //    throw new FahrtberechnungException(400, "User with such username already exists");

            var existUser = await GetAsync(u => u.Username == login && u.Password == password.Encrypt());

            if (existUser == null)
                throw new FahrtberechnungException(400, "Login or Password is incorrect");

            var user = existUser.Adapt<User>();

            user.UpdatedAt = DateTime.UtcNow;

            user = unitOfWork.Users.Update(user = userForUpdateDto.Adapt(user));
            await unitOfWork.SaveChangesAsync();
            return user.Adapt<UserViewDto>();
        }
    }
}
