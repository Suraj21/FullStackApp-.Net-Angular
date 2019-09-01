using FriendBookApp.API.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FriendBookApp.API.Data.SeedData
{
    public class Seed
    {
        public readonly DataContext dataContext;
        public Seed(DataContext dbContext)
        {
            dataContext = dbContext;
        }

        public void SeedUser()
        {
            var userData = System.IO.File.ReadAllText("Data/SeedData/UserSeedData.json");
            var users = JsonConvert.DeserializeObject<List<Users>>(userData);
            foreach (var user in users)
            {
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash("password", out passwordHash, out passwordSalt);
                user.Passwordhash = passwordHash;
                user.Passwordsalt = passwordSalt;

                user.Username = user.Username.ToLower();
                dataContext.Add<Users>(user);
            }

            dataContext.SaveChanges();
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (System.Security.Cryptography.HMACSHA512 hMACSHA512 = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hMACSHA512.Key;
                passwordHash = hMACSHA512.ComputeHash(System.Text.ASCIIEncoding.UTF8.GetBytes(password));
            }
        }
    }
}
