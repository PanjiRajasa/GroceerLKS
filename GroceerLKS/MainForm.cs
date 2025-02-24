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
            //make all of the SessionManager data become null
            SessionManager.PhoneNumber = null;
            SessionManager.Password = null;
            SessionManager.Role = null;

            //hide the current form first
            this.Hide();

            //go to the login form
            Form1 form1 = new Form1();

            //close the program if we close the new form
            form1.FormClosing += (s, args) => this.Close();
            form1.Show();
        }

        //to got to the profile menu
        private void buttonProfile_Click(object sender, EventArgs e)
        {
            //make an instance of the profile form
            Profile profile = new Profile();

            //hide the current form
            this.Hide();

            //if the new form closed, current form will show again
            profile.FormClosing += (s, args) => this.Show();
            profile.Show();
        }
    }
}
