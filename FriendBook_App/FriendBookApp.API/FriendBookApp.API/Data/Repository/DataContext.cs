using FriendBookApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace FriendBookApp.API.Data
{
    public class DataContext:DbContext
    {
        public DbSet<Value> Value { get; set; }
        public DbSet<Users> Users { get; set; }
        public DataContext(DbContextOptions<DataContext> options):base(options)
        {
        }
    }
}
