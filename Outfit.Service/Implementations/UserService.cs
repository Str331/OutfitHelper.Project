using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Outfit.DAL.Interfaces;
using Outfit.Domain;
using Outfit.Domain.Entity;
using Outfit.Domain.Extensions;
using Outfit.Domain.Helpers;
using Outfit.Domain.Response;
using Outfit.Domain.ViewModels.User;
using Outfit.Service.Service_Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Outfit.Service.Implementations
{
    public class UserService:IUserService
    {
        private readonly ILogger<UserService> _logger;
        private readonly IBaseRepository<Profile> _profileRepository;
        private readonly IBaseRepository<User> _userRepository;

        public UserService(ILogger<UserService> logger,IBaseRepository<User> userRepository,
            IBaseRepository<Profile> profileRepository)
        {
            _logger = logger;
            _profileRepository = profileRepository;
            _userRepository = userRepository;
        }

        public async Task<IBaseResponse<User>> Create(UserViewModel model)
        {
            try
            {
                var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Name == model.Name);
                if (user != null)
                {
                    return new BaseResponse<User>()
                    {
                        Description="Пользователь с данным логином уже существует",
                        StatusCode = Domain.StatusCode.UserAlreadyExists
                    };
                }
                user = new User()
                {
                    Name=model.Name,
                    Role = Enum.Parse<Role>(model.Role),
                    Password = HashPasswordHelper.HashPassword(model.Password)
                };
                await _userRepository.Create(user);

                var profile = new Profile()
                {
                    Address = string.Empty,
                    Age=0,
                    UserId=user.Id
                };
                await _profileRepository.Create(profile);

                return new BaseResponse<User>()
                {
                    Data=user,
                    Description = "Пользователь добавлен",
                    StatusCode=StatusCode.OK
                };
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"[UserService.Create] error: {ex.Message}");
                return new BaseResponse<User>()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"Внутренняя ошибка: {ex.Message}"
                };
            }
        }

        public BaseResponse<Dictionary<int,string>> GetRoles()
        {
            try
            {
                var roles = ((Role[])Enum.GetValues(typeof(Role)))
                    .ToDictionary(k => (int)k, t => t.GetDisplayName());

                return new BaseResponse<Dictionary<int, string>>()
                {
                    Data = roles,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Dictionary<int, string>>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<IEnumerable<UserViewModel>>> GetUsers()
        {
            try
            {
                var users = await _userRepository.GetAll()
                    .Select(x => new UserViewModel()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Role = x.Role.GetDisplayName()
                    })
                    .ToListAsync();

                _logger.LogInformation($"[UserService.GetUsers] получено элементов {users.Count}");
                return new BaseResponse<IEnumerable<UserViewModel>>()
                {
                    Data=users,
                    StatusCode = StatusCode.OK
                };
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"[UserSerivce.GetUsers] error: {ex.Message}");
                return new BaseResponse<IEnumerable<UserViewModel>>()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"Внутренняя ошибка: {ex.Message}"
                };
            }
        }

        public async Task<IBaseResponse<bool>> DeleteUser(long id)
        {
            try
            {
                var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if(user == null)
                {
                    return new BaseResponse<bool>()
                    {
                        Data = false,
                        StatusCode = StatusCode.UserNotFound
                    };
                }
                await _userRepository.Delete(user);
                _logger.LogInformation($"[UserService.DeleteUser] пользователь удален");

                return new BaseResponse<bool>
                {
                    Data = true,
                    StatusCode = StatusCode.OK
                };
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"[UserSerivce.DeleteUser] error: {ex.Message}");
                return new BaseResponse<bool>()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"Внутренняя ошибка: {ex.Message}"
                };
            }
        }
    }
}
