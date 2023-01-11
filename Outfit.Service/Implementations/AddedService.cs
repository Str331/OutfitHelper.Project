using Microsoft.EntityFrameworkCore;
using Outfit.DAL.Interfaces;
using Outfit.Domain;
using Outfit.Domain.Entity;
using Outfit.Domain.Response;
using Outfit.Domain.ViewModels.Added;
using Outfit.Service.Service_Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outfit.Service.Implementations
{
    public class AddedService : IAddedService
    {
        private readonly IBaseRepository<User> _userRepository;
        private readonly IBaseRepository<Added> _addedRepository;

        public AddedService(IBaseRepository<User> userRepository,IBaseRepository<Added> addedRepository)
        {
            _userRepository = userRepository;
            _addedRepository = addedRepository;
        }

        public async Task<IBaseResponse<Added>> Create(CreateAddedViewModel model)
        {
            try
            {
                var user = await _userRepository.GetAll()
                    .Include(x => x.Favorite)
                    .FirstOrDefaultAsync(x => x.Name == model.Login);
                if(user == null)
                {
                    return new BaseResponse<Added>()
                    {
                        Description="Пользователь не найден",
                        StatusCode = StatusCode.UserNotFound
                    };
                }

                var added = new Added()
                {
                    DateofAdd = DateTime.Now,
                    FavoriteId = user.Favorite.Id,
                    ClothesId = model.ClothesId
                };

                await _addedRepository.Create(added);

                return new BaseResponse<Added>()
                {
                    Description = "Образ добавлен",
                    StatusCode = StatusCode.OK
                };
            }
            catch(Exception ex)
            {
                return new BaseResponse<Added>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<bool>> Delete(long id)
        {
            try
            {
                var added = _addedRepository.GetAll()
                    .Select(x => x.Favorite.Addeds.FirstOrDefault(y => y.Id == id))
                    .FirstOrDefault();

                if(added == null)
                {
                    return new BaseResponse<bool>()
                    {
                        Description = "Контент не найден",
                        StatusCode = StatusCode.ContentNotFound
                    };
                }

                await _addedRepository.Delete(added);
                return new BaseResponse<bool>()
                {
                    Description = "Образ удален",
                    StatusCode= StatusCode.OK
                };
            }
            catch(Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
    }
}
