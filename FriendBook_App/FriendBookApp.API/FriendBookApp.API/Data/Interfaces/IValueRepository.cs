using FriendBookApp.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FriendBookApp.API.Data.Interfaces
{
    public interface IValueRepository : IRepository<Value>
    {
        Task<List<Value>> GetValues();

        Task<Value> GetValue(int id);

        void SaveValue(Value value);
    }
}
