using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GroceerLKS
{
    public partial class Form1: Form
    {

        //import data from  the dbml file
        //make the connection
        DataClasses1DataContext db = new DataClasses1DataContext();
        //make a public variable 
        public static user User { get; private set; }

        public Form1()
        {
            InitializeComponent();
            textBoxPassword.PasswordChar = '*'; //make the password textBox have a '*' string format
            labelError.Visible = false; //hide the error label
        }

        //when user press the sign in button
        private void buttonSignIn_Click(object sender, EventArgs e)
        {
            //reformat the textBoxPhone.Text to avoid bugs!
            string phoneNumber = textBoxPhone.Text.Trim();
            string password = textBoxPassword.Text;

            //validation before the db validation
            //if the textBoxes are empty
            if( Utils.isEmptyWhiteSpaceString(textBoxPhone.Text) ||  Utils.isEmptyWhiteSpaceString(textBoxPassword.Text))
            {
                labelError.Visible = true;
                labelError.Text = "All column must be filled!";
                return;
            } 
            //if the phone number is not valid
            else if (!(Utils.isValidPhoneNumber(textBoxPhone.Text)))
            {
                labelError.Visible = true;
                labelError.Text = "Phone number must be digits and 10 - 15 characters long!";
                return;
            }
            //if user not select any role
            else if (comboBoxRole.SelectedItem == null)
            {
                //anticipate so the user won't leave the checkbox blank
                labelError.Visible = true;
                labelError.Text = "Empty role!";
                return;
            }


            //variable to check if the phone number and the password are match
            //use FirstOrDefault because we only need one data, which is the logged user's data
            User = (from s in db.users where (s.phone_number == phoneNumber && s.password == password) select s).FirstOrDefault();

            //if user credentials are wrong
            if(User == null)
            {
                labelError.Visible = true;
                labelError.Text = "Phone number or password are wrong!";
                return;
            }

            //filter the role using the combobox
            string selectedOption = comboBoxRole.SelectedItem.ToString();

            //decide the data that we will receive based on the user's choice

            //if the user select the customer role but in the db their role is not valid (inactive customer)
            if(selectedOption == "customer" && User.cust_active != 1)
            {
                labelError.Visible = true;
                labelError.Text = "Unauthorized login!";
                return;
            }
            //if the user select the vendor role but in the db their role is not valid (inactive vendor)
            else if (selectedOption == "vendor" && User.vendor_active != 1)
            {
                labelError.Visible = true;
                labelError.Text = "Unauthorized login!";
                return;
            } 
            
            //if the user exist (password, phone number, role are match with the db)
            if(User != null)
            {
                //role checking
                bool isValidRole = (selectedOption == "customer" && User.cust_active == 1) ||
                                   (selectedOption == "vendor" && User.vendor_active == 1);

                //related user
                var loggedId = (from s in db.users where s.id == User.id select s.id).FirstOrDefault();

                //if the role is valid
                if(isValidRole)
                {   
                    //save the login data to the session manager
                    SessionManager.Login(phoneNumber, password, selectedOption, loggedId);

                    //hide current form first
                    this.Hide();

                    //move to the mainform
                    MainForm mainForm = new MainForm();

                    //close the program if we close the new form
                    mainForm.FormClosing += (s, args) => this.Close();

                    //display the new form
                    mainForm.Show();
                }
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {   
            //hide the current form first
            this.Hide();

            //make an instance of the new form
            SignUp signUp = new SignUp();

            //display the form again, if we close the signup form
            signUp.FormClosing += (s, args) => this.Show();

            //display the new form
            signUp.Show();
        }
    }
}
