using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UpsideDownKitten.DL
{
    public class UsersDao
    {
        private static readonly List<User> _users = new List<User>();

        public UsersDao()
        {
            
        }

        public void CreateUser(User user)
        {
            _users.Add(user);
        }

        public bool Login(string email, string password)
        {
            return _users.Any(x=>x.Email == email && x.Password == password);
        }

    }
}
