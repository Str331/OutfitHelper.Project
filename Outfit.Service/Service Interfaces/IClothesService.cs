using Outfit.Domain.Entity;
using Outfit.Domain.Response;
using Outfit.Domain.ViewModels.Clothes;
using Outfit.Service.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Outfit.Service.Service_Interfaces
{
    public interface IClothesService
    {
        BaseResponse<Dictionary<int, string>> GetTypes();
        IBaseResponse<List<Clothes>> GetClothes();
        Task<IBaseResponse<ClothesViewModel>> GetOneClothes(long id);
        Task<BaseResponse<Dictionary<long, string>>> GetOneClothes(string term);
        Task<IBaseResponse<Clothes>> CreateClothes(ClothesViewModel model, byte[] imageData);
        Task<IBaseResponse<bool>> DeleteClothes(long id);
        Task<IBaseResponse<Clothes>> Edit(long id, ClothesViewModel model);
    }
}
