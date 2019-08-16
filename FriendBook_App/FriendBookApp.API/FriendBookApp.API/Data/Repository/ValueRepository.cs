using FriendBookApp.API.Data.Interfaces;
using FriendBookApp.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FriendBookApp.API.Data.Repository
{
    public class ValueRepository: Repository<Value>, IValueRepository
    {
        public ValueRepository(DataContext dataContext):base(dataContext)
        {
            DataContext = dataContext;
        }

        public new DataContext DataContext { get; }

        public async Task<List<Value>> GetValues()
        {
            return await DataContext.Value.ToListAsync();
        }

        public async Task<Value> GetValue(int id)
        {
            return await DataContext.Value.FirstOrDefaultAsync(x => x.Id == id);
        }

        public void SaveValue(Value value)
        {
            try
            {
                 DataContext.Value.Add(value);
                 DataContext.SaveChanges();
            }
            catch (System.Exception ex)
            {

                throw;
            }
        }
    }
}
