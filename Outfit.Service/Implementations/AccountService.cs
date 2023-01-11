using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Outfit.DAL.Interfaces;
using Outfit.Domain;
using Outfit.Domain.Entity;
using Outfit.Domain.Helpers;
using Outfit.Domain.Response;
using Outfit.Domain.ViewModels.Account;
using Outfit.Service.Service_Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Outfit.Service.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly IBaseRepository<Profile> _profileRepository;
        private readonly IBaseRepository<User> _userRepository;
        private readonly ILogger<AccountService> _logger;

        public AccountService(IBaseRepository<User> userRepository,
            ILogger<AccountService> logger, IBaseRepository<Profile> profileRepository)
        {
            _userRepository = userRepository;
            _logger = logger;
            _profileRepository = profileRepository;
        }

        public async Task<BaseResponse<ClaimsIdentity>> Register(RegisterViewModel model)
        {
            try
            {
                var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Name == model.Name);
                if(user != null)
                {
                    return new BaseResponse<ClaimsIdentity>()
                    {
                        Description = "Пользователь с данным логином уже существует",
                    };
                }

                user = new User()
                {
                    Name= model.Name,
                    Role = Role.User,
                    Password = HashPasswordHelper.HashPassword(model.Password),
                };

                var prodile = new Profile()
                {
                    UserId = user.Id
                };

                await _userRepository.Create(user);
                await _profileRepository.Create(prodile);
                var result = Authenticate(user);

                return new BaseResponse<ClaimsIdentity>()
                {
                    Data = result,
                    Description = "Обьект добавлен",
                    StatusCode = StatusCode.OK
                };
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"[Register]: {ex.Message}");
                return new BaseResponse<ClaimsIdentity>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<ClaimsIdentity>> Login(LoginViewModel model)
        {
            try
            {
                var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Name == model.Name);
                if(user == null)
                {
                    return new BaseResponse<ClaimsIdentity>()
                    {
                        Description = "Пользователь не найден"
                    };
                }
                if(user.Password != HashPasswordHelper.HashPassword(model.Password))
                {
                    return new BaseResponse<ClaimsIdentity>()
                    {
                        Description = "Неверный логин или пароль"
                    };
                }
                var result = Authenticate(user);

                return new BaseResponse<ClaimsIdentity>()
                {
                    Data = result,
                    StatusCode = StatusCode.OK
                };
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"[Login] : {ex.Message}");
                return new BaseResponse<ClaimsIdentity>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<bool>> ChangePassword(ChangePasswordViewModel model)
        {
            try
            {
                var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Name == model.UserName);
                if(user == null)
                {
                    return new BaseResponse<bool>()
                    {
                        StatusCode = StatusCode.UserNotFound,
                        Description = "Такого пользователя нет"
                    };
                }
                user.Password = HashPasswordHelper.HashPassword(model.NewPassword);
                await _userRepository.Update(user);

                return new BaseResponse<bool>()
                {
                    Data = true,
                    StatusCode = StatusCode.OK,
                    Description = "Пароль обновлён"
                };
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"[ChangePassword] : {ex.Message}");
                return new BaseResponse<bool>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
        private ClaimsIdentity Authenticate(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Name),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.ToString())
            };
            return new ClaimsIdentity(claims, "ApplicationCookie",
                ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
        }
    }
}
