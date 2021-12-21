using HuskyNet.Models.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HuskyNet.Data.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected DbContext _db;

        public DbSet<T> Set { get; private set; }

        public Repository(ApplicationDbContext db)
        {
            _db = db;
            var set =_db.Set<T>();
            set.Load();

            Set = set;
        }

        public async Task Create(T item)
        {
            await Task.Run(() => Set.Add(item));
            _db.SaveChanges();
        }

        public async Task Delete(T item)
        {
            await Task.Run(() => Set.Remove(item));
            _db.SaveChanges();
        }

        public async Task<T> Get(int id)
        {
            return await Task<T>.Run(() => Set.Find(id));
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await Task<T>.Run(() => Set);
        }

        public async Task Update(T item)
        {
            await Task.Run(() => Set.Update(item));
            _db.SaveChanges();
        }
    }
}
