using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GroceerLKS
{
    public partial class MainForm: Form
    {
        //make a data connection
        DataClasses1DataContext db = new DataClasses1DataContext();

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //select the user data where his phone number == phone number from the login form result
            var user = (from s in db.users where s.phone_number == SessionManager.PhoneNumber select s).FirstOrDefault();

            if(user != null)
            {
                //if the role is customer, we will use the customer's data, if not we will use the vendor's data
                string nameData = SessionManager.Role == "customer" ? user.cust_name : user.vendor_name;

                labelUser.Text = nameData;
                labelRole.Text = SessionManager.Role;
            }
        }

        private void buttonLogout_Click(object sender, EventArgs e)
        {
            //Logout
            SessionManager.Logout();

            //hide the current form first
            this.Hide();

            //go to the login form
            Form1 form1 = new Form1();

            //close the program if we close the new form
            form1.FormClosing += (s, args) => this.Close();
            form1.Show();
        }

        //to go to the profile menu
        private void buttonProfile_Click(object sender, EventArgs e)
        {
            //make an instance of the profile form
            Profile profile = new Profile();

            //hide the current form
            this.Hide();

            //if the new form closed, current form will show again
            profile.FormClosing += (s, args) => this.Show();
            //showing the new form
            profile.Show();
        }

        //go to the product form
        private void buttonProduct_Click(object sender, EventArgs e)
        {   
            //display the product form based on the role
            if(SessionManager.Role == "customer")
            {
                //make an instance of the customer product form
                CustomerProducts customerProducts = new CustomerProducts();

                //hide the current form
                this.Hide();

                //if the new form closed, current form will show again
                customerProducts.FormClosing += (s, args) => this.Show();
                //showing the new form
                customerProducts.Show();
            } else if(SessionManager.Role == "vendor")
            {
                //make an instance of the vendor form 
                VendorProducts vendorProducts = new VendorProducts();

                //hide the current form
                this.Hide();

                //if the new form closed, current form will show again, here I use a FormClosing method
                vendorProducts.FormClosing += (s, args) => this.Show();
                //showing the new form
                vendorProducts.Show();
            }
        }

        //to go the transaction form
        private void buttonTransaction_Click(object sender, EventArgs e)
        {
            //display the transaction form based on the role
            if(SessionManager.Role == "customer")
            {
                TransactionCustomer transactionCustomer = new TransactionCustomer(); //new form instance

                this.Hide(); //hide the current form

                transactionCustomer.FormClosing += (s, args) => this.Show(); //new form closed = current form will showing again

                transactionCustomer.Show(); //show the current form
            } else if(SessionManager.Role == "vendor")
            {
                TransactionVendor transactionVendor = new TransactionVendor(); //new form instance

                this.Hide(); //hide the current form

                transactionVendor.FormClosing += (s, args) => this.Show(); //new form closed = current form will showing again

                transactionVendor.Show(); //show the current form
            }
        }
    }
}
