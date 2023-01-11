using Outfit.DAL.Interfaces;
using Outfit.DAL.Repositories;
using Outfit.Domain.Entity;
using Outfit.Service.Implementations;
using Outfit.Service.Service_Interfaces;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using NuGet.ContentModel;
using Outfit.DAL.Interfaces;
using Outfit.Domain.Entity;

namespace Outfit
{
    public static class Initializer
    {
        public static void InitializeRepositories(this IServiceCollection services)
        {
            services.AddScoped<IBaseRepository<Clothes>, ClothesRepository>();
            services.AddScoped<IBaseRepository<User>, UserRepository>();
            services.AddScoped<IBaseRepository<Profile>, ProfileRepository>();
            services.AddScoped<IBaseRepository<Favorite>, FavoriteRepository>();
            services.AddScoped<IBaseRepository<Added>, AddedRepository>();
        }

        public static void InitializeServices(this IServiceCollection services)
        {
            services.AddScoped<IClothesService, ClothesService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IProfileService, ProfileService>();
            services.AddScoped<IFavoriteService, FavoriteService>();
            services.AddScoped<IAddedService, AddedService>();
        }
    }
}