using Outfit.Domain.Entity;
using Outfit.Domain.Response;
using Outfit.Domain.ViewModels.Added;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outfit.Service.Service_Interfaces
{
    public interface IAddedService
    {
        Task<BaseResponse<Added>> Create(CreateAddedViewModel model);

        Task<IBaseResponse<bool>> Delete(long id);
    }
}
