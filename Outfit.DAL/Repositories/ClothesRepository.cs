using Microsoft.EntityFrameworkCore;
using Outfit.DAL.Interfaces;
using Outfit.Domain;
using Outfit.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Outfit.DAL.Repositories
{
    public class ClothesRepository : IBaseRepository<Clothes>
    {
        private readonly ApplicationDbContext _db;

        public ClothesRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        //создание
        public async Task  Create(Clothes entity)
        {
            await _db.Clothes.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        //получение
        public IQueryable<Clothes> GetAll()
        {
            return _db.Clothes;
        }

        //удаление
        public async Task Delete(Clothes entity)
        {
            _db.Clothes.Remove(entity);
            await _db.SaveChangesAsync();
        }

        //обновление
        public async Task<Clothes> Update(Clothes entity)
        {
            _db.Clothes.Update(entity);
            await _db.SaveChangesAsync();

            return entity;
        }
    }
}
