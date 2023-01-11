using Microsoft.EntityFrameworkCore;
using Outfit.DAL.Interfaces;
using Outfit.Domain;
using Outfit.Domain.Entity;
using Outfit.Domain.Extensions;
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
    public class FavoriteService : IFavoriteService
    {
        private readonly IBaseRepository<User> _userRepository;
        private readonly IBaseRepository<Clothes> _clothesRepository;

        public FavoriteService(IBaseRepository<User> userRepository,IBaseRepository<Clothes> clothesRepository)
        {
            _userRepository = userRepository;
            _clothesRepository = clothesRepository;
        }

        public async Task<IBaseResponse<IEnumerable<AddedViewModel>>> GetItems(string userName)
        {
            try
            {
                var user = await _userRepository.GetAll()
                    .Include(x => x.Favorite)
                    .ThenInclude(x => x.Addeds)
                    .FirstOrDefaultAsync(x => x.Name == userName);

                if(user == null)
                {
                    return new BaseResponse<IEnumerable<AddedViewModel>>()
                    {
                        Description = "Пользователь не найден",
                        StatusCode = StatusCode.UserNotFound
                    };
                }

                var addeds = user.Favorite?.Addeds;
                var response = from p in addeds
                               join c in _clothesRepository.GetAll() on p.ClothesId equals c.Id
                               select new AddedViewModel()
                               {
                                   Id=p.Id,
                                   OutfitName = c.Name,
                                   OutfitType = c.outfitType.GetDisplayName(),
                               };

                return new BaseResponse<IEnumerable<AddedViewModel>>()
                {
                    Data = response,
                    StatusCode = StatusCode.OK
                };
            }
            catch(Exception ex)
            {
                return new BaseResponse<IEnumerable<AddedViewModel>>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<AddedViewModel>> GetOneItem(string userName,long id)
        {
            try
            {
                var user = await _userRepository.GetAll()
                    .Include(x => x.Favorite)
                    .ThenInclude(x => x.Addeds)
                    .FirstOrDefaultAsync(x => x.Name == userName);

                if (user == null)
                {
                    return new BaseResponse<AddedViewModel>()
                    {
                        Description = "Пользователь не найден",
                        StatusCode = StatusCode.UserNotFound
                    };
                }

                var addeds = user.Favorite?.Addeds.Where(x => x.Id == id).ToList();
                if(addeds == null || addeds.Count == 0)
                {
                    return new BaseResponse<AddedViewModel>()
                    {
                        Description = "Нет избранного контента",
                        StatusCode = StatusCode.ContentNotFound
                    };
                }

                var response = (from p in addeds
                                join c in _clothesRepository.GetAll() on p.ClothesId equals c.Id
                                select new AddedViewModel()
                                {
                                    Id = p.Id,
                                    OutfitName = c.Name,
                                    OutfitType = c.outfitType.GetDisplayName(),
                                    DateOfAdd = p.DateofAdd.ToLongDateString()
                                }).FirstOrDefault();

                return new BaseResponse<AddedViewModel>()
                {
                    Data = response,
                    StatusCode = StatusCode.OK
                };
            }
            catch(Exception ex)
            {
                return new BaseResponse<AddedViewModel>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
    }
}
