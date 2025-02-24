using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceerLKS
{
    public static class SessionManager
    {
        public static string PhoneNumber { get; set; }
        public static string Password { get; set; }
        public static string Role { get; set; }

        //method to logout 
        public static void Logout() {
            PhoneNumber = null;
            Password = null;
            Role = null;
        }
    }
}
