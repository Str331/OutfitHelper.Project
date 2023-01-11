using Outfit.DAL.Interfaces;
using Outfit.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outfit.DAL.Repositories
{
    public class AddedRepository : IBaseRepository<Added>
    {
        private readonly ApplicationDbContext _db;

        public AddedRepository(ApplicationDbContext db) => _db = db;

        public async Task Create(Added entity)
        {
            await _db.Addeds.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public IQueryable<Added> GetAll() => _db.Addeds;

        public async Task Delete(Added entity)
        {
            _db.Addeds.Remove(entity);
            await _db.SaveChangesAsync();
        }

        public async Task<Added> Update(Added entity)
        {
            _db.Addeds.Update(entity);
            await _db.SaveChangesAsync();

            return entity;
        }
    }
}
