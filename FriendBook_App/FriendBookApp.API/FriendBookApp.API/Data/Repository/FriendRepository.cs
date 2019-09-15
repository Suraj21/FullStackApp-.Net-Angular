using FriendBookApp.API.Data.Interfaces;
using FriendBookApp.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FriendBookApp.API.Data.Repository
{
    public class FriendRepository : IFriendRepository
    {
        private readonly DataContext dataContext;
        public FriendRepository(DataContext context)
        {
            dataContext = context;
        }
        public void Add<T>(T entity) where T : class
        {
            dataContext.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            dataContext.Remove(entity);
        }

        public async Task<Photo> GetPhoto(int id)
        {
            var photo = await dataContext.Photos.FirstOrDefaultAsync(p => p.Id == id);

            return photo;
        }

        public async Task<Users> GetUser(int id)
        {
            var user = await dataContext.Users.Include(p => p.Photos).FirstOrDefaultAsync(u => u.Id == id);
            return user;
        }

        public async Task<IEnumerable<Users>> GetUsers()
        {
            var users = await dataContext.Users.Include(p => p.Photos).ToListAsync();
            return users;
        }

        public async Task<bool> SaveAll()
        {
            return await dataContext.SaveChangesAsync() > 0;
        }
    }
}
