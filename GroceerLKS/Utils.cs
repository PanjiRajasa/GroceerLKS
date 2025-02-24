using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GroceerLKS
{
    internal class Utils
    {   
        public static bool IsCoordinateDigitValid(params TextBox[] coordinateTextBox)
        {

            foreach(var textBox in coordinateTextBox)
            {
                string coor = textBox.Text;

                //skip textBox coordinate inside a groupBox that disabled
                if(string.IsNullOrWhiteSpace(coor))
                {
                    continue;
                }

                //if the coordinate only contains '-' or '.'
                if(coor.All(c => c == '-') || coor.All(c => c == '.')) {
                    return false;
                }

                //if the coordinate contains more than one '-' , contains '-' but not in the begining of the line
                if(coor.Count(c => c == '-') > 1 || (coor.Contains('-') && coor[0] != '-'))
                {
                    return false;
                }

                //if the coordinate only contains '.' but more than one '.'
                if(coor.Count(c => c == '.') > 1)
                {
                    return false;
                }

                //check if the coordinate satify the coordinate format
                //valid coordinate example -89.977, 89.900 -> there's '-' but only one and only at the beginning, contain '.' but not in the beginning, and most importantly contain digit (0-9)
                if(!coor.All(c => char.IsDigit(c) || c == '.' || c == '-'))
                {
                    return false;
                }
            }

            //return true mean, everything it's okay
            return true;
        }

        public static bool isValidPhoneNumber(string phoneNumber)
        {
            return phoneNumber.All(char.IsDigit) && phoneNumber.Length >= 10 && phoneNumber.Length <= 15;
        }

        public static bool isEmptyWhiteSpaceString(string text)
        {
            return string.IsNullOrWhiteSpace(text);
        }

        public static bool isValidPasswordPattern(string password)
        {
            return password.Any(char.IsUpper) && password.Any(char.IsLower) && password.Any(char.IsDigit) && password.Length >= 8;
        }

        public static bool isPasswordMatch(string password, string confirmPassword)
        {
            return password == confirmPassword;
        }

        public static bool isValidEmail(string email)
        {   
            try
            {
                var address = new MailAddress(email);
                return address.Address == email;
            } catch
            {
                return false;
            }
            
        }
    }
}
