using Outfit.DAL.Interfaces;
using Outfit.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outfit.DAL.Repositories
{
    public class FavoriteRepository : IBaseRepository<Favorite>
    {
        private readonly ApplicationDbContext _db;

        public FavoriteRepository(ApplicationDbContext db) => _db = db;

        public async Task Create(Favorite entity)
        {
            await _db.Favorites.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public IQueryable<Favorite> GetAll() => _db.Favorites;

        public async Task Delete(Favorite entity)
        {
            _db.Favorites.Remove(entity);
            await _db.SaveChangesAsync();
        }

        public async Task<Favorite> Update(Favorite entity)
        {
            _db.Favorites.Update(entity);
            await _db.SaveChangesAsync();

            return entity;
        }
    }
}
