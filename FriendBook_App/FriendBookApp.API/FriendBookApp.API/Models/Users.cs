using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FriendBookApp.API.Models
{
    public class Users
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public byte[] Passwordhash { get; set; }
        public byte[] Passwordsalt { get; set; }
    }
}
