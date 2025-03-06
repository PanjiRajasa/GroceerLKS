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
    public partial class Profile: Form
    {
        //make a database connection
        DataClasses1DataContext db = new DataClasses1DataContext();

        //to check all of the groupBoxes members wheter it's empty or not
        //IEnumerable<dataType> is used for an iteration of the specific dataType
        private bool AreGroupBoxesValid(IEnumerable<GroupBox> groupBoxes)
        {
            //how these codes work, first we use Where() function to filter, only enabled gropboxes that we count
            //SelectMany -> select many(...)
            //Here we select all of the controls inside the groupBox after the filter.
            //the controls that we will select are TextBox and RichTextBox
            //so we type gb.Controls.OfType<type>
            //because more than 1 control, we use concat<dataTYpe>
            //so it will become like this:
            //SelectMany(gb => gb.Controls.OfType<TextBox>() 
            //.Concat<Control>(gb.Controls.OfType<RichTextBox>() ) )
            //All(...) checks that every groupBoxes aren't empty
            //we use the Utils internal class method to perform this one, you can also use !string.IsNullOrWhiteSpace
            //inside the all function we will use the Text inside the groupBox
            return groupBoxes
                .Where(gb => gb.Enabled)
                .SelectMany(gb => gb.Controls.OfType<TextBox>()
                .Concat<Control>(gb.Controls.OfType<RichTextBox>()))
                .All(gb => !Utils.isEmptyWhiteSpaceString(gb.Text));

            //!Utils.isEmptyWhiteSpaceString(gb.Text) -> logic explaination:
            /*
                if !(true) -> there is an empty control, then it will reversed to false, then when we access the function, we will use negation/not operator (!) again so it will become true, and the error validation can be executed

                for example: !Utils.isEmptyWhiteSpaceString(gb.Text) -> !(true) -> false -> !AreGroupBoxesValid( groupBoxes) -> !false -> true -> error validation will be executed

                  !Utils.isEmptyWhiteSpaceString(gb.Text) -> !(false) -> true -> 
                    !AreGroupBoxesValid( groupBoxes) -> !true -> false -> no error validation will be executed because no error that detected
             */
        }

        //the global object that holds the user's data from this form, this method is optional, because, with all of the methods that I had created, this form should function perfectly
        //optional
        public static Profile instance;
        //optional

        public Profile()
        {
            InitializeComponent();

            //whenever this form is created, it will save the data inside this instance and we can access this instance in another form
            //this is same like how the SessionManager class works
            instance = this;
        }

        //function that executed when the form loaded
        private void Profile_Load(object sender, EventArgs e)
        {
            //default form components configuration
            labelErrorProfile.Visible = false;
            textBoxPhoneProfile.Enabled = false;
            groupBoxCustomer.Enabled = false;
            groupBoxVendor.Enabled = false;
            buttonEditProfile.Enabled = false;

            //select the user data where his phone number == phone number from the login
            var user = (from s in db.users where s.phone_number == SessionManager.PhoneNumber select s).FirstOrDefault();

            if (user != null)
            {
                //set the data
                textBoxPhoneProfile.Text = user.phone_number;
                textBoxEmailProfile.Text = user.email;

                //we use cust_active and vendor_active to control the checkbox.checked
                //user.cust_active == 1 or user.vendor_active == 1 statements return a boolean value
                checkBoxCustomer.Checked = user.cust_active == 1;
                checkBoxVendor.Checked = user.vendor_active == 1;

                //control the groupBox datas
                if(checkBoxCustomer.Checked)
                {
                    //enabled the groupBox
                    groupBoxCustomer.Enabled = true;
                }

                //set the groupBox data
                textBoxCustomerName.Text = user.cust_name;
                richTextBoxCustomerAddress.Text = user.cust_address;
                textBoxLatitudeCustomer.Text = user.cust_latitude.ToString();
                textBoxLongitudeCustomer.Text = user.cust_longitude.ToString();

                if (checkBoxVendor.Checked)
                {
                    //enabled the groupBox
                    groupBoxVendor.Enabled = true;
                }

                //set the groupBox data
                textBoxVendorName.Text = user.vendor_name;
                richTextBoxVendorAddress.Text = user.vendor_address;
                textBoxLatitudeVendor.Text = user.vendor_latitude.ToString();
                textBoxLongitudeVendor.Text = user.vendor_longitude.ToString();
            }
        }

        //back to the main form and discard all of the new changes
        private void buttonDiscard_Click(object sender, EventArgs e)
        {
            //cancel every progress
            db.Refresh(System.Data.Linq.RefreshMode.OverwriteCurrentValues, db.users);

            //make an instance of the main form
            MainForm mainForm = new MainForm();

            //hide the current form first
            this.Hide();

            //if the new form closed, the current form will show again
            mainForm.FormClosing += (s, args) => this.Close();

            //showing the main form
            mainForm.Show();
            
        }

        //enable or disable groupBoxes based on the checkBoxes CheckedChange event
        private void checkBoxCustomer_CheckedChanged(object sender, EventArgs e)
        {
            //based on the checkbox checked status -> true/false
            groupBoxCustomer.Enabled = checkBoxCustomer.Checked;
        }

        //enable or disable groupBoxes based on the checkBoxes CheckedChange event
        private void checkBoxVendor_CheckedChanged(object sender, EventArgs e)
        {   
            //based on the checkbox checked status -> true/false
            groupBoxVendor.Enabled = checkBoxVendor.Checked;
        }

        //if the user press the save button
        private void buttonSave_Click(object sender, EventArgs e)
        {

            //variable that holds the checkBox status, true or false
            //.Checked event can be true or false
            bool checkBoxCustomerStatus = checkBoxCustomer.Checked;
            bool checkBoxVendorStatus = checkBoxVendor.Checked;

            //groupBox list, we store the groupBoxes inside a List, so it will make easier to use to loop through it using the AreGroupBoxesValid method
            var groupBoxList = new List<GroupBox> { groupBoxCustomer, groupBoxVendor };

            //general checking
            //email must be filled
            if(Utils.isEmptyWhiteSpaceString(textBoxEmailProfile.Text))
            {
                labelErrorProfile.Visible = true;
                labelErrorProfile.Text = "Email must be filled!";
                return;
            } 
            //if the email format does not valid
            else if(!Utils.isValidEmail(textBoxEmailProfile.Text))
            {
                labelErrorProfile.Visible = true;
                labelErrorProfile.Text = "Email does not have proper format!";
                return;
            } 
            //if use does not choose any role
            else if(!checkBoxCustomerStatus && !checkBoxVendorStatus)
            {
                labelErrorProfile.Visible = true;
                labelErrorProfile.Text = "Select at least one role!";
                return;
            }
            //groupBox checking
            else if(!AreGroupBoxesValid(groupBoxList))
            {
                labelErrorProfile.Visible = true;
                labelErrorProfile.Text = "All field must be filled!";
                return;
            }
            //coordinate checking
            else if(!Utils.IsCoordinateDigitValid(textBoxLatitudeCustomer, textBoxLongitudeCustomer, textBoxLatitudeVendor, textBoxLongitudeVendor))
            {
                labelErrorProfile.Visible = true;
                labelErrorProfile.Text = "Invalid coordinate format. Use numbers like - 6.178 or 106.762";
                return;
            }

            //if everything is OK, keep the labelError invisible
            labelErrorProfile.Visible = false;

            //make a variable that holds the user data
            //select the user data where his phone number == phone number from the login
            var user = (from s in db.users where s.phone_number == SessionManager.PhoneNumber select s).FirstOrDefault();

            //update logic start here

            //first we will make variables that represent the data that we will update to the database

            //email
            string newEmail = null;
            //role
            short cust_active = 0;
            short vendor_active = 0;
            //customer data
            string cust_name = null;
            string cust_address = null;
            double? cust_latitude = null;
            double? cust_longitude = null;
            //vendor data
            string vendor_name = null;
            string vendor_address = null;
            double? vendor_latitude = null;
            double? vendor_longitude = null;

            //set the data based on whether the groupBoxes enabled or disabled
            //here, in the update logic, we will only update the data that changed, not everything beside what we changed

            //set the email
            newEmail = textBoxEmailProfile.Text;

            //if the groupBox customer enabled, we will set the role to be customer
            if(groupBoxCustomer.Enabled)
            {   
                //activate the customer role
                cust_active = 1;
                
            }

            //wheter the groupBox enabled or not, the data will still remains even if we disable the role
            cust_name = textBoxCustomerName.Text;
            cust_address = richTextBoxCustomerAddress.Text;
            cust_latitude = double.Parse(textBoxLatitudeCustomer.Text);
            cust_longitude = double.Parse(textBoxLongitudeCustomer.Text);

            //if the groupBox vendor enabled
            if (groupBoxVendor.Enabled)
            {   
                //activate the vendor role
                vendor_active = 1;
            }
            //wheter the groupBox enabled or not, the data will still remains even if we disable the role
            vendor_name = textBoxVendorName.Text;
            vendor_address = richTextBoxVendorAddress.Text;
            vendor_latitude = double.Parse(textBoxLatitudeVendor.Text);
            vendor_longitude = double.Parse(textBoxLongitudeVendor.Text);

            //update the user's data
            //if the user exist
            if(user != null)
            {   
                //update the general data
                user.email = newEmail;

                //update the role
                user.cust_active = cust_active;
                user.vendor_active = vendor_active;

                //update the customer's data
                user.cust_name = cust_name;
                user.cust_address = cust_address;
                user.cust_latitude = cust_latitude;
                user.cust_longitude = cust_longitude;

                //update the vendor's data
                user.vendor_name = vendor_name;
                user.vendor_address = vendor_address;
                user.vendor_latitude = vendor_latitude;
                user.vendor_longitude = vendor_longitude;

                //timeStamps, only use the updated_at because we only update the existing user, not insert a new user data
                user.updated_at = DateTime.Now;

                //submit the data, use try cacth to perevent error
                try {db.SubmitChanges(); }
                catch (Exception ex) { Console.WriteLine($"{ex.Message}"); }
                
             }

            //control the transaction status and product status, based on the role status (0 inactive / 1 active)

            //customer's logic

            //checkboxCustomer control the customer status, if the checkBoxCustomer.Checked == false it means, they have reactivated their role

            //customer role

            //if reactivated the customer role, all of the pending transactions will be cancelled
            if (!checkBoxCustomer.Checked)
            {
                //pending transaction customer
                var pendingTransaction = (from s in db.transactions where s.customer_id == user.id && s.status == "pending" select s).ToList();

                foreach (var transaction in pendingTransaction)
                {
                    transaction.status = "abort";
                }


                //submit the data, use try cacth to perevent error
                try { db.SubmitChanges(); }
                catch (Exception ex) { Console.WriteLine($"{ex.Message}"); }
            }

            //vendor role

            //if reactivated vendor role, all of the pending transactions will be cancelled/abort and all of their product listing will be inactive
            if (!checkBoxVendor.Checked)
            {
                //pending transaction customer
                var pendingTransaction = (from s in db.transactions where s.vendor_id == user.id && s.status == "pending" select s).ToList();
                //active products listing for the vendor
                var activeProducts = (from s in db.products where s.vendor_id == user.id && s.is_active == 1 select s).ToList();

                foreach(var transaction in pendingTransaction)
                {
                    transaction.status = "abort";
                }
                
                foreach(var product in activeProducts)
                {
                    product.is_active = 0;
                }


                //submit the data, use try cacth to perevent error
                try { db.SubmitChanges(); }
                catch (Exception ex) { Console.WriteLine($"{ex.Message}"); }
            }


            //after updating the data, user will forced to go to the login page again and we will clear the SessionManager's data
            SessionManager.Logout();

            //we will hide the current form first
            this.Hide();

            //make a login form instance
            Form1 form1 = new Form1();

            //close the program if we close the new form
            form1.FormClosed += (s, args) => this.Close();

            //show the form
            form1.Show();
        }
    }
}
