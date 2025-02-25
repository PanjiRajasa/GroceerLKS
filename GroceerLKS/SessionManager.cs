using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceerLKS
{
    public static class SessionManager
    {
        public static string PhoneNumber { get; private set; }
        public static string Password { get; private set; }
        public static string Role { get; private set; }

        //method to logout 
        public static void Logout() {
            PhoneNumber = null;
            Password = null;
            Role = null;
        }

        //method to login
        public static void Login(string phone, string password, string role)
        {
            PhoneNumber = phone;
            Password = password;
            Role = role;
        }
    }
}
