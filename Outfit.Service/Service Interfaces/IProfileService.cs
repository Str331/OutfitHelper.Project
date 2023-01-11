using Outfit.Domain.Entity;
using Outfit.Domain.Response;
using Outfit.Domain.ViewModels.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outfit.Service.Service_Interfaces
{
    public interface IProfileService
    {
        Task<BaseResponse<ProfileViewModel>> GetProfile(string UserName);

        Task<BaseResponse<Profile>> Save(ProfileViewModel model);
    }
}
