using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceerLKS
{   
    //Session manager is used to store the user's credential information
    public static class SessionManager
    {   
        //save the phone number
        public static string PhoneNumber { get; private set; }
        //save the password
        public static string Password { get; private set; }
        //save the role
        public static string Role { get; private set; }
        //save the ID
        public static int ID { get; private set; }

        //method to logout 
        public static void Logout() {
            PhoneNumber = null;
            Password = null;
            Role = null;
            ID = 0;
        }

        //method to login
        public static void Login(string phone, string password, string role, int id)
        {
            PhoneNumber = phone;
            Password = password;
            Role = role;
            ID = id;
        }
    }
}
