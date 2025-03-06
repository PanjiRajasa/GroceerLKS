using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace GroceerLKS
{
    public partial class SignUp: Form
    {

        //import data from  the dbml file
        //make the connection
        DataClasses1DataContext db = new DataClasses1DataContext();
        //user's data
        public static user User { get; private set; }

        public SignUp()
        {
            InitializeComponent();

            //default windows form component configuration
            //all group box enabled = false
            groupBoxCustomerDetails.Enabled = false;
            groupBoxVendorDetails.Enabled = false;
            //label error enabled = false
            labelErrorSignUp.Visible = false;
            //password char set to the '*' char
            textBoxPasswordSP.PasswordChar = '*';
            textBoxConfirmPassSP.PasswordChar = '*';

        }

        //control the groupbox behavior based on the CheckedChanged event
        private void checkBoxCustomerSP_CheckedChanged(object sender, EventArgs e)
        {
            //Control which group box needs to be displayed based on the checkbox. If the checkbox is true (checked), then the corresponding group box will be enabled.
            groupBoxCustomerDetails.Enabled = checkBoxCustomerSP.Checked;
        }

        private void checkBoxVendorSP_CheckedChanged(object sender, EventArgs e)
        {
            //control which group box that need to be displayed based on the checkbox, the logic is same as the previous logic
            groupBoxVendorDetails.Enabled = checkBoxVendorSP.Checked;
        }


        //all of the groupbox's members will be required if it's enabled
        private bool AreGroupBoxesValid(params GroupBox[] groupBoxes)
        {
            //loop all of the groupBoxes
            foreach (GroupBox groupBox in groupBoxes)
            {

                //enabled = false -> inactive
                if (!groupBox.Enabled)
                {
                    continue; //If there's an inactive groupBox, skip
                }

                //loop all of the controls inside the groupBoxes -> using the groupBox parameter variable
                foreach(Control ctrl in groupBox.Controls)
                {   
                    //if the control is a TextBox
                    if(ctrl is TextBox textbBox && string.IsNullOrWhiteSpace(textbBox.Text))
                    {
                        return false; //There is an empty TextBox

                    }
                    //if the control is a RichTextBox
                    else if(ctrl is RichTextBox richTextBox && string.IsNullOrWhiteSpace(richTextBox.Text))
                    {

                        return false; //There is an empty RichTextBox
                    }
                }
            }

            return true; //it means, everything inside the groupBoxes is valid and we can move to the next checking
        }

        //when the register button is clicked
        private void buttonRegister_Click(object sender, EventArgs e)
        {
            //reformat the user's input 
            string phoneNumber = textBoxPhoneSP.Text.Trim();
            string email = textBoxEmailSP.Text.Trim();
            string password = textBoxPasswordSP.Text;
            string confirmPassword = textBoxConfirmPassSP.Text;

            bool checkBoxCustomer = checkBoxCustomerSP.Checked;
            bool checkBoxVendor = checkBoxVendorSP.Checked;

            //general checking
            //if they are empty or just containing a whitespace
            if ( Utils.isEmptyWhiteSpaceString(phoneNumber) ||    
                Utils.isEmptyWhiteSpaceString(email) || 
                Utils.isEmptyWhiteSpaceString(password) || 
                Utils.isEmptyWhiteSpaceString(confirmPassword))
            {
                labelErrorSignUp.Visible = true;
                labelErrorSignUp.Text = "All column must be filled!";
                return;
            }
            //if the phone number format is not valid
            else if (!(Utils.isValidPhoneNumber(phoneNumber)))
            {
                labelErrorSignUp.Visible = true;
                labelErrorSignUp.Text = "Phone number must be digits\r\nand 10 - 15 characters long!";
                return;
            } 
            //if the password pattern is not valid (password must contains a combination of uppercase, lowercase, and numbers with the length minimum 8 characters) 
            else if(!(Utils.isValidPasswordPattern(password))) {
                labelErrorSignUp.Visible = true;
                labelErrorSignUp.Text = "Password must be a combination of uppercase, lowercase characters,\r\nand numbers with length minimum 8 characters!";
                return;
            } 
            //if both password testBoxes are matching
            else if(!(Utils.isPasswordMatch(password, confirmPassword)))
            {
                labelErrorSignUp.Visible = true;
                labelErrorSignUp.Text = "Passwords confirmation doesn't match\r\nwith the inputted password.";
                return;
            } 
            //if the email format is valid
            else if(!(Utils.isValidEmail(email)))
            {
                labelErrorSignUp.Visible = true;
                labelErrorSignUp.Text = "Email format does not have proper format!";
                return;
            } 
            //if user not check either the vendor checkbox or the customer checkbox
            else if(!(checkBoxCustomer) && !(checkBoxVendor))
            {
                labelErrorSignUp.Visible = true;
                labelErrorSignUp.Text = "Select at least one role!";
                return;
            } 
            //if there's an enabled groupBox, but the groupBox controls is empty
            else if(!AreGroupBoxesValid(groupBoxCustomerDetails, groupBoxVendorDetails))
            {
                labelErrorSignUp.Visible = true;
                labelErrorSignUp.Text = "All columns must be filled!";
                return;
            } 

            //if the coordinate is not valid
            if(!Utils.IsCoordinateDigitValid(textBoxLatitudeCustomerSP, textBoxLongitudeSP, textBoxLatitudeCustomerSP, textBoxLongitudeCustomerSP))
            {
                labelErrorSignUp.Visible = true;
                labelErrorSignUp.Text = "Invalid coordinate format. Use numbers like - 6.178 or 106.762";
                return;
            }

            //spesific logic checking
            //select the existing phone number in order to avoid duplicate data
            var existingPhoneNumber = (from s in db.users where (s.phone_number == phoneNumber) select s.phone_number).FirstOrDefault();

            //if the phone number already exist
            if(existingPhoneNumber == phoneNumber)
            {
                labelErrorSignUp.Visible = true;
                labelErrorSignUp.Text = "Phone number already exist!";
                return;
            }

            
            //if the inputs do not have any errors
            labelErrorSignUp.Visible = false;

            //create a new user logic

            //customer groupBox
            string CustomerName = textBoxCustomerName.Text;
            string CustomerAddress = richTextBoxCustomerAddress.Text;
            string LatitudeCustomer = textBoxLatitudeCustomerSP.Text;
            string LongitudeCustomer = textBoxLongitudeCustomerSP.Text;

            //vendor groupBox
            string VendorName = textBoxVendorName.Text;
            string VendorAddress = richTextBoxVendorAddress.Text;
            string VendorLatitude = textBoxLatitudeSP.Text;
            string VendorLongitude = textBoxLongitudeSP.Text;

            
            //general data
            string newPhoneNumber = phoneNumber;
            string newEmail = email;
            string newUserPass = password;

            //post logic
            //Set the role & user data
            // Set default values
            short cust_active = 0;
            short vendor_active = 0;

            // Variabel untuk Customer (default: null)
            string cust_name = null;
            string cust_address = null;
            double? cust_latitude = null;
            double? cust_longitude = null;

            // Variabel untuk Vendor (default: null)
            string vendor_name = null;
            string vendor_address = null;
            double? vendor_latitude = null;
            double? vendor_longitude = null;


            //if the customer groupBox enabled = true
            if (groupBoxCustomerDetails.Enabled)
            {
                cust_active = 1;
                cust_name = CustomerName;
                cust_address = CustomerAddress;
                cust_latitude = double.Parse(LatitudeCustomer);
                cust_longitude = double.Parse(LongitudeCustomer);
            }
            //if the vendor groupBox enabled = true 
            if (groupBoxVendorDetails.Enabled)
            {
                vendor_active = 1;
                vendor_name = VendorName;
                vendor_address = VendorAddress;
                vendor_latitude = double.Parse(VendorLongitude);
                vendor_longitude = double.Parse(VendorLongitude);
            }

            //create a new user
            using(db)
            {
                int newId = 1;
                //first result from the order by descending id = last id, then use the FirstOrDefault to get the first data.
                //for example, id -> 1,2,3,4. OrderByDescending makes the id -> 4,3,2,1. FirstOrDefault only select 4 -> 4 is the last ID
                var lastUser = (from s in db.users.OrderByDescending(u => u.id) select s.id).FirstOrDefault();

                //use this if the current post program returns an error
                //if(lastUser != null)
                //{
                //    newId = lastUser + 1;
                //}
                newId = lastUser + 1;

                User = new user
                {
                    //general data
                    id = newId,
                    phone_number = newPhoneNumber,
                    email = newEmail,
                    password = newUserPass,

                    //role
                    cust_active = cust_active,
                    vendor_active = vendor_active,

                    //vendor data
                    vendor_name = vendor_name,
                    vendor_address = vendor_address,
                    vendor_latitude = vendor_latitude,
                    vendor_longitude = vendor_longitude,

                    //customer data
                    cust_name = cust_name,
                    cust_address = cust_address,
                    cust_latitude = cust_latitude,
                    cust_longitude = cust_longitude,

                    //Timestamps
                    created_at = DateTime.Now,
                    updated_at = DateTime.Now
                };

                //insert to the Database
                db.users.InsertOnSubmit(User);
                db.SubmitChanges();
            }

            //navigate to the login form
            this.Hide();
            Form1 form1 = new Form1();

            //close the program if we close the new form
            form1.FormClosing += (s, args) => this.Close();
            //showing the new form
            form1.Show();

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void labelErrorSignUp_Click(object sender, EventArgs e)
        {

        }
    }
}
