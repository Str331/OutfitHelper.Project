using Outfit.Domain.Entity;
using Outfit.Domain.Response;
using Outfit.Domain.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outfit.Service.Service_Interfaces
{
    public interface IUserService
    {
        Task<IBaseResponse<User>> Create(UserViewModel model);

        BaseResponse<Dictionary<int, string>> GetRoles();

        Task<BaseResponse<IEnumerable<UserViewModel>>> GetUsers();

        Task<IBaseResponse<bool>> DeleteUser(long id);
    }
}
