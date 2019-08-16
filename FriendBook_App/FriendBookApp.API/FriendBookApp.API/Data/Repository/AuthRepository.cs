using FriendBookApp.API.Data.Interfaces;
using FriendBookApp.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace FriendBookApp.API.Data.Repository
{
    public class AuthRepository : IAuthRepository
    {
        public AuthRepository(DataContext dataContext)
        {
            DataContext = dataContext;
        }

        private readonly DataContext DataContext;

        public async Task<Users> Login(string userName, string password)
        {
            var user = await DataContext.Users.FirstOrDefaultAsync(x => x.Username == userName);

            if (user == null) return null;

            if (!VerifyPasswordHash(password, user.Passwordhash, user.Passwordsalt))
                return null;

            return user;
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (System.Security.Cryptography.HMACSHA512 hMACSHA512 = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hMACSHA512.ComputeHash(System.Text.ASCIIEncoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i]) return false;
                }
            }
            return true;
        }

        public async Task<Users> Register(Users user, string password)
        {
            try
            {
                CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
                user.Passwordhash = passwordHash;
                user.Passwordsalt = passwordSalt;

                await DataContext.Users.AddAsync(user);
                await DataContext.SaveChangesAsync();

                return user;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (System.Security.Cryptography.HMACSHA512 hMACSHA512 = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hMACSHA512.Key;
                passwordHash = hMACSHA512.ComputeHash(System.Text.ASCIIEncoding.UTF8.GetBytes(password));
            }
        }

        public async Task<bool> UserExists(string userName)
        {
            if (await DataContext.Users.AnyAsync(x => x.Username == userName))
                return true;

            return false;
        }
    }
}
