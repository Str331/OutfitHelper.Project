using Outfit.Domain.Response;
using Outfit.Domain.ViewModels.Added;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outfit.Service.Service_Interfaces
{
    public interface IFavoriteService
    {
        Task<IBaseResponse<IEnumerable<AddedViewModel>>> GetItems(string userName);

        Task<IBaseResponse<AddedViewModel>> GetOneItem(string userName, long id);
    }
}
