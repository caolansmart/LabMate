using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabMate
{
    internal class User
    {
        internal string Username { get; set; }
        internal string Password { get; set; }
        internal bool IsLoggedIn { get; set; }
        internal User(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}
