using FriendBookApp.API.Models;
using System;
using System.Threading.Tasks;

namespace FriendBookApp.API.Data.Interfaces
{
    public interface IAuthRepository
    {
        Task<Users> Register(Users user, string password);
        Task<Users> Login(String userName, string password);
        Task<bool> UserExists(string userName);
    } 
}
