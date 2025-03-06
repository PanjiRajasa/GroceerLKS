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
        //check if the coordinate is valid or not, params keyword allow you to store many similiar iterable object, here we will check multiple TextBox
        public static bool IsCoordinateDigitValid(params TextBox[] coordinateTextBox)
        {
            //loop through the params
            foreach(var textBox in coordinateTextBox)
            {
                string coor = textBox.Text; //textBox refers to the coordinateTextBox, then it refers to the TextBox object that represent the windows TextBox component

                //skip textBox coordinate inside a groupBox that disabled
                if (string.IsNullOrWhiteSpace(coor))
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

        //to check something is digit
        public static bool isDigit(string text)
        {
            return text.All(char.IsDigit);
        }

        //validate the phone number
        public static bool isValidPhoneNumber(string phoneNumber)
        {   
            //phone number must be digits, and have length >= 10 - length <= 15
            return phoneNumber.All(char.IsDigit) && phoneNumber.Length >= 10 && phoneNumber.Length <= 15;
        }

        //this method is used to check whether the component is null or only containing a white space
        public static bool isEmptyWhiteSpaceString(string text)
        {
            return string.IsNullOrWhiteSpace(text);
        }

        //to check the password pattern
        public static bool isValidPasswordPattern(string password)
        {   
            //password must contain atleast one upper case, one lower chase, one digit, and have length >= 8
            return password.Any(char.IsUpper) && password.Any(char.IsLower) && password.Any(char.IsDigit) && password.Length >= 8;
        }

        //to check whether the two password variable is match or not
        public static bool isPasswordMatch(string password, string confirmPassword)
        {
            return password == confirmPassword;
        }

        //to validate the email format
        public static bool isValidEmail(string email)
        {   
            //every time you use an object, it recommended to use a try catch block in order to avoid bugs and error happen.
            try
            {
                var address = new MailAddress(email); //MailAddress is an object that represents an email. If the parameter inside the object has the same format as an email address, it will return true, otherwise, it will return false.
                return address.Address == email; //make sure for the second time that address.Address (the property is used to access/get the parameter inside the object when the object instance was created) == email (method parameter)
            } catch
            {
                return false; //If an error happens, we will return a false value.
            }
            
        }
    }
}
