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
    public partial class CustomerProducts: Form
    {
        public CustomerProducts()
        {
            InitializeComponent();
        }

        //make the data connection
        DataClasses1DataContext db = new DataClasses1DataContext();

        //when the form load
        private void CustomerProducts_Load(object sender, EventArgs e)
        {
            //form component's default configuration

            //disabled the error label by default
            labelError.Visible = false;
            //disabled the button buy 
            buttonBuy.Enabled = false;

            //product details groupBox will always be disabled
            groupBoxDetails.Enabled = false;

            //select the item's category from the database
            var category = from s in db.categories select s;
            //select the product's data from the database
            var products = from s in db.products select s;

            //load the data

            //make the object instance
            ActiveProduct activeProd = new ActiveProduct();

            var activeProduct = activeProd.GetActiveProducts();

            //bind the data to the DataGridView
            dataGridViewProducts.DataSource = activeProduct.ToList();

            //hide the coordinate column
            dataGridViewProducts.Columns["vendor_latitude"].Visible = false;
            dataGridViewProducts.Columns["vendor_longitude"].Visible = false;

            //hide the ID column
            dataGridViewProducts.Columns["productID"].Visible = false;
            dataGridViewProducts.Columns["vendorID"].Visible = false;

            //Here are settings to make columns and rows of the DataGridView resize automatically
            //auto resize columns
            dataGridViewProducts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            //auto resize rows
            dataGridViewProducts.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

        }

        //product id public
        public int productID { get; private set; }

        //vendor id public
        public int vendorID { get; private set; }

        //display the data products inside the details & transactional groupBox when you clicked the gridView CellClick event
        private void dataGridViewProducts_CellClick(object sender, DataGridViewCellEventArgs e)
        {   
            // 'e' is a variable which contains the information of the cell that was clicked in the DataGridView

            //show the buy button
            buttonBuy.Enabled = true;

            //make sure that clicked row is valid
            if(e.RowIndex >= 0) //not a minus index, minus index will make the program crash
            {
                // DataGridViewRow -> An object that represents a row inside a DataGridView in Windows Forms (WinForms).
                // Here, we create an instance of that object and store it inside the 'row' variable.
                // We use this object to access the specific row inside 'dataGridViewProducts'.
                //
                // The DataGridView component is structured like a list or an array,
                // so we need to use bracket notation [index] to access a specific row by its index.
                //
                // The index is obtained from the 'e.RowIndex' parameter, which represents the row
                // that was clicked by the user inside the DataGridView.
                //
                // Once we have the row, we can access its data using the 'Cells' property, 
                // which contains the values of each column in that row.
                DataGridViewRow row = dataGridViewProducts.Rows[e.RowIndex];

                //assign the data to the form components
                //product name TextBox
                textBoxProductName.Text = row.Cells["product_name"].Value.ToString();
                //products category comboBox
                comboBoxCategoryProducts.Text = row.Cells["category_name"].Value.ToString();
                //radio button result
                string unit_type = row.Cells["unit_type"].Value.ToString().ToLower();
                //use condition to determine which unitType that we will choose
                if(unit_type == "countable")
                {
                    radioButtonCountable.Checked = true;
                } else if(unit_type == "measurable")
                {
                    radioButtonMeasurable.Checked = true;
                }

                //price per unit numericUpDown

                //To avoid exception range bugs, here we will set the minimum and maximum value of the numericUpDown value based on the minimum and maximum value, here I'll use 1 ad the minimal value and then infinity value as the maximum value
                decimal minimalValue = 0;
                decimal maxValue = decimal.MaxValue; //decimal.MaxValue -> infinity

                //assign the minimalValue and the maximumValue to the Minimum and Maximum properties
                numericUpDownPriceUnit.Minimum = minimalValue;
                numericUpDownPriceUnit.Maximum = maxValue;

                //assign the row.Cells["price_per_unit"] value to the numericUpDown, don't forget to convert it to the decimal first!
                numericUpDownPriceUnit.Value = Convert.ToDecimal(row.Cells["price_per_unit"].Value);


                //unit stock numericUpDown

                //The logic of this component is same just like how the price per unit's numericUpDown works

                //determine the maximum and minimum values, same explanation like how the previous numeric up down minimum and maximum value logic
                decimal minimalUnit = 0;
                decimal maxUnit = decimal.MaxValue;

                //determine the number of digits after the decimal point of the numeric up down unitStock
                //NumericUpDown by default only handles integers, so decimal values ​​will be rounded automatically.
                numericUpDownDetailsUnitStock.DecimalPlaces = 2;
                numericUpDownQuantity.DecimalPlaces = 2;

                //assign the minimal value and the maximum value to the Minimum and Maximum properties
                numericUpDownDetailsUnitStock.Maximum = maxUnit;
                numericUpDownDetailsUnitStock.Minimum = minimalUnit;

                //assign the row.Cells["unit_stock"] value to the numericUpDown, here we also need to convert it to the decimal first
                numericUpDownDetailsUnitStock.Value = Convert.ToDecimal(row.Cells["unit_stock"].Value);

                //numeric up down quantity minimum value, avoid user buys negative items (-1,-2, and so on)
                numericUpDownQuantity.Minimum = 0;

                //update the label total transaction
                CalculateTotalTransaction();

                //display the delivery cost logic
                //first we need the user's data
                var user = (from s in db.users where s.phone_number == SessionManager.PhoneNumber select s).FirstOrDefault();

                //calculate the delivery cost
                if (user != null)
                {
                    //for the coordinates, we use the cells value
                    decimal vendorLatitude = Convert.ToDecimal(row.Cells["vendor_latitude"].Value);
                    decimal vendorLongitude = Convert.ToDecimal(row.Cells["vendor_longitude"].Value);
                    //for the customer's coordinate, we use the user variable data
                    decimal customerLatitude = (decimal)user.cust_latitude;
                    decimal customerLongitude = (decimal)user.cust_longitude;

                    //calculate the distance
                    decimal distance = Delivery.CalculateDeliveryCost(vendorLatitude, vendorLongitude, customerLatitude, customerLongitude);
                    labelDeliveryCost.Text = distance.ToString();
                }

                //global productID variable saves the product's id
                productID = int.Parse(row.Cells["productID"].Value.ToString());
                //global vendorID variable saves the vendor's id
                vendorID = int.Parse(row.Cells["vendorID"].Value.ToString());
            }
        }


        //when we press the buy button
        private void buttonBuy_Click(object sender, EventArgs e)
        {
            //numericUpDownQuantity logic
            //if the numeric up down quantity value is higher than the unit stock numeric up down value (user try to buy a product but with quantity that higher than the product stock -> returns an error)-> returns an error

            if (numericUpDownQuantity.Value > numericUpDownDetailsUnitStock.Value)
            {
                labelError.Visible = true;
                labelError.Text = "Quantity cannot exceed available stock!";
                return;
            }

            //if quantity is 0 or empty
            if(numericUpDownQuantity.Value <= 0)
            {
                labelError.Visible = true;
                labelError.Text = "Insufficient stock!";
                return;
            }

            //make sure that the user's transaction amount isn't more than 10 transaction
            PendingTransaction pendingTransaction = new PendingTransaction();
            var pendingTran = pendingTransaction.GetPendingTransaction();

            //if the user's transaction > 10, the user can't make a transaction until the vendor approve it or the user cancel it
            if(pendingTran > 10)
            {
                labelError.Visible = true;
                labelError.Text = "You cannot have more than 10 pending transactions.";
                return;
            }

            //if there's no error
            labelError.Visible = false;

            //add a new transaction record (set the status to pending status)
            var transaction = new transaction();

            if (transaction != null)
            {
                transaction.id = (db.users.OrderByDescending(s => s.id).Select(s => s.id).FirstOrDefault()) + 1; //select the last ID then increment it (+1)
                transaction.status = "pending";
                transaction.total_price = Convert.ToDecimal(labelTotal.Text);
                transaction.quantity = (double)numericUpDownQuantity.Value;
                transaction.product_id = productID;
                transaction.customer_id = (db.users.Where(s => s.id == SessionManager.ID).Select(s => s.id)).FirstOrDefault();
                transaction.vendor_id = vendorID;
                transaction.delivery_cost = Convert.ToDecimal(labelDeliveryCost.Text);

                //timestamps
                transaction.created_at = DateTime.Now;
                transaction.updated_at = DateTime.Now;

                //deduct the product quantity/stock
                //search product where its ID == selected product ID
                var selectedProduct = db.products.FirstOrDefault(s => s.id == transaction.product_id);

                //if the selectedProduct is not null and the unit stock is higher than 0
                if(selectedProduct != null && selectedProduct.unit_stock > 0)
                {
                    selectedProduct.unit_stock -= transaction.quantity;
                }

                //insert and submit the data. We use the try catch block to avoid bugs and errors
                try
                {
                    //insert the new data to the database
                    db.transactions.InsertOnSubmit(transaction);

                    //submit to the database
                    db.SubmitChanges();

                } catch (Exception ex)
                {
                    db.Refresh(System.Data.Linq.RefreshMode.OverwriteCurrentValues, db.transactions); //refresh the db
                    Console.WriteLine($"\n\n{ex.Message}\n\n"); //display the error inside the console (debugging purpose)
                }

                //refresh the dataGrid view

                //load the data again

                //make the object instance
                ActiveProduct activeProd = new ActiveProduct();

                var activeProduct = activeProd.GetActiveProducts();

                //set the dataGridView data
                dataGridViewProducts.DataSource = activeProduct.ToList();
            }
        }

        //event when we change the numeric up down quantity value
        private void numericUpDownQuantity_ValueChanged(object sender, EventArgs e)
        {
            //function to calculate the total transaction
            CalculateTotalTransaction();

            //function to calculate the delivery cost
            
        }

        //calculate the total transaction
        private void CalculateTotalTransaction()
        {
            //total transaction formula
            decimal quantity = numericUpDownQuantity.Value;
            decimal price = numericUpDownPriceUnit.Value;
            decimal total = quantity * price;

            labelTotal.Text = total.ToString();
        }

        //clear logic
        private void buttonClear_Click(object sender, EventArgs e)
        {
            //disabled the buy button
            buttonBuy.Enabled = false;

            //clear all the components, make every text empty and every number reset to 0
            textBoxProductName.Text = ""; //name

            comboBoxCategoryProducts.Text = ""; //category 

            radioButtonCountable.Checked = false; //countable radio

            radioButtonMeasurable.Checked = false; //measurable radio

            numericUpDownPriceUnit.Value = 0; //price per unit

            numericUpDownDetailsUnitStock.Value = 0; //unit stock

            labelDeliveryCost.Text = ""; //delivery cost
        }
    }
}
