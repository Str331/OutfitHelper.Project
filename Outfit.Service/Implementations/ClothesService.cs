using Microsoft.EntityFrameworkCore;
using Outfit.DAL.Interfaces;
using Outfit.Domain;
using Outfit.Domain.Entity;
using Outfit.Domain.Response;
using Outfit.Domain.ViewModels.Clothes;
using Outfit.Service.Service_Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outfit.Service.Implementations
{
    public class ClothesService : IClothesService
    {
        private readonly IBaseRepository<Clothes> _clothesRepository;
        public ClothesService(IBaseRepository<Clothes> clothesRepository)
        {
            _clothesRepository = clothesRepository;
        }
        //Получение обьекта по разновидности
        public BaseResponse<Dictionary<int, string>> GetTypes()
        {
            try
            {
                var types = ((OutfitType[])Enum.GetValues(typeof(OutfitType)))
                    .ToDictionary(k => (int)k, t => t.GetDisplayName());

                return new BaseResponse<Dictionary<int, string>>()
                {
                    Data = types,
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
        //Получение обьекта по Id
        public async Task<IBaseResponse<ClothesViewModel>> GetOneClothes(long id)
        {
            try
            {
                var clothes = await _clothesRepository.GetAll().FirstOrDefaultAsync(x=>x.Id==id);
                if (clothes == null)
                {
                    return new BaseResponse<ClothesViewModel>()
                    {
                        Description = "Пользователь не найден",
                        StatusCode = StatusCode.UserNotFound
                    };
                }

                var data = new ClothesViewModel()
                {
                    DateOfAdd = clothes.DateOfAdd,
                    Description = clothes.Description,
                    Name = clothes.Name,
                    Price = clothes.Price,
                    outfitType = clothes.outfitType.ToString(),
                    Image = clothes.avatar,
                };
                return new BaseResponse<ClothesViewModel>()
                {
                    StatusCode = StatusCode.OK,
                    Data = data
                };
            } catch (Exception ex)
            {
                return new BaseResponse<ClothesViewModel>()
                {
                    Description = $"[GetOneClothes] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<Dictionary<long,string>>> GetOneClothes(string term)
        {
            var baseResponse = new BaseResponse<Dictionary<long, string>>();
            try
            {
                var clothes = await _clothesRepository.GetAll()
                    .Select(x => new ClothesViewModel()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Description = x.Description,
                        DateOfAdd = x.DateOfAdd.ToLongDateString(),
                        Price = x.Price,
                        outfitType = x.outfitType.GetDisplayName()
                    })
                    .Where(x => EF.Functions.Like(x.Name, $"%{term}%"))
                    .ToDictionaryAsync(x => x.Id, t => t.Name);

                baseResponse.Data = clothes;
                return baseResponse;
            }
            catch(Exception ex)
            {
                return new BaseResponse<Dictionary<long, string>>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
        
        //Удаление обьекта по Id
        public async Task<IBaseResponse<bool>> DeleteClothes(long id)
        {
            try
            {
                var clothes = await _clothesRepository.GetAll().FirstOrDefaultAsync(x=>x.Id == id);
                if (clothes == null)
                {
                    return new BaseResponse<bool>()
                    {
                        Description = "User not found",
                        StatusCode = StatusCode.UserNotFound,
                        Data = false
                    };
                }
                await _clothesRepository.Delete(clothes);
                return new BaseResponse<bool>()
                {
                    Data = true,
                    StatusCode = StatusCode.OK
                };
            }catch(Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[DeleteClothes] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
        //Создание обьекта
        public async Task<IBaseResponse<Clothes>> CreateClothes(ClothesViewModel model, byte[] imageData)
        {
            try
            {
                var clothes = new Clothes()
                {
                    Name = model.Name,
                    Description = model.Description,
                    Price = model.Price,
                    DateOfAdd = DateTime.Now,
                    outfitType = (OutfitType)Convert.ToInt32(model.outfitType),
                    avatar = imageData
                };
                await _clothesRepository.Create(clothes);
                return new BaseResponse<Clothes>()
                {
                    StatusCode = StatusCode.OK,
                    Data = clothes
                };
            }catch(Exception ex)
            {
                return new BaseResponse<Clothes>()
                {
                    Description = $"[CreateClothes] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
        //Получение всех обьектов
        public IBaseResponse<List<Clothes>> GetClothes()
        {
            try 
            {
                var clothes = _clothesRepository.GetAll().ToList();
                if (!clothes.Any())
                {
                    return new BaseResponse<List<Clothes>>()
                    {
                        Description = "Найдено 0 элементов",
                        StatusCode = StatusCode.OK
                    };
                }
                return new BaseResponse<List<Clothes>>()
                {
                    Data = clothes,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<Clothes>>()
                {
                    Description = $"[GetClothes] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
        //изменение обьекта
        public async Task<IBaseResponse<Clothes>> Edit(long id, ClothesViewModel model)
        {
            try
            {
                var clothes =await _clothesRepository.GetAll().FirstOrDefaultAsync(x=>x.Id==id);
                if(clothes == null)
                {
                    return new BaseResponse<Clothes>()
                    {
                        Description = "Образ не найден",
                        StatusCode = StatusCode.ClothesNotFound
                    };
                }
                clothes.Description = model.Description;
                clothes.Price = model.Price;
                clothes.DateOfAdd = DateTime.ParseExact(model.DateOfAdd, "yyyyMMdd HH:mm", null);
                clothes.Name = model.Name;
                await _clothesRepository.Update(clothes);

                return new BaseResponse<Clothes>()
                {
                    Data = clothes,
                    StatusCode = StatusCode.OK
                };
                
            }catch(Exception ex)
            {
                return new BaseResponse<Clothes>()
                {
                    Description = $"[Edit] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
    }
}
