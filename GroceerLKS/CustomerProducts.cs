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
            //disabled the button buy and clear by default
            buttonBuy.Enabled = false;
            buttonClear.Enabled = false;

            //product details groupBox will always be disabled
            groupBoxDetails.Enabled = false;

            //select the item's category from the database
            var category = from s in db.categories select s;
            //select the product's data from the database
            var products = from s in db.products select s;

            //load the data

            //dataGridView data
            //Here I use the join query to combine product + user + categories with some selections

            //from the products table, join it with the users table on vendor id from the products table equal to the id from the user, then join it with the categories table on category id from the products table equal to the user id from the users table
            //where (criteria that indicate whether the products is active or not, is_active from the products table == 1, unit_stock from the products table more than 0 -> unit_stock > 0, and the last, vendor_active status is == 1 from the users table
            //then we will display a new object which contains vendor_name,product_name,name (category), unit_type, price_per_unit, unit_stock
            var activeProduct = from p in db.products
                                join v in db.users on p.vendor_id equals v.id
                                join c in db.categories on p.category_id equals c.id
                                where p.is_active == 1
                                && p.unit_stock > 0
                                && v.vendor_active == 1
                                select new
                                {
                                    v.vendor_name,
                                    p.product_name,
                                    c.name,
                                    p.unit_type,
                                    p.price_per_unit,
                                    p.unit_stock
                                };
            //bind the data to the DataGridView
            dataGridViewProducts.DataSource = activeProduct.ToList();

            //Here are settings to make columns and rows of the DataGridView resize automatically
            //auto resize columns
            dataGridViewProducts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            //auto resize rows
            dataGridViewProducts.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

        }

        //display the data products inside the details & transactional groupBox when you clicked the gridView CellClick event
        private void dataGridViewProducts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //show the buy button
            buttonBuy.Enabled = true;

            //make sure that clicked row is valid
            if(e.RowIndex >= 0) //not a minus index
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
                comboBoxCategoryProducts.Text = row.Cells["name"].Value.ToString();
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

                //in order to avoid exception range bugs, here we will set the minimum and maximum value of the numericUpDown value based on the minimum and maximum value of the price value in database

                //here we are using Max and Min function
                decimal minimalValue = db.products.Min(p => Convert.ToDecimal(p.price_per_unit));
                decimal maxValue = db.products.Max(p => Convert.ToDecimal(p.price_per_unit));

                //assign the minimalValue and the maximumValue to the Minimum and Maximum properties
                numericUpDownPriceUnit.Minimum = minimalValue;
                numericUpDownPriceUnit.Maximum = maxValue;

                //assign the row.Cells["price_per_unit"] value to the numericUpDown, don't forget to convert it to the decimal first!
                numericUpDownPriceUnit.Value = Convert.ToDecimal(row.Cells["price_per_unit"].Value);


                //unit stock numericUpDown

                //The logic of this component is same just like how the price per unit's numericUpDown works

                //determine the maximum and minimum values
                decimal minimalUnit = db.products.Min(p => Convert.ToDecimal(p.unit_stock));
                decimal maxUnit = db.products.Max(p => Convert.ToDecimal(p.unit_stock));

                //determine the number of digits after the decimal point of the numeric up down unitStock
                //NumericUpDown by default only handles integers, so decimal values ​​will be rounded automatically.
                numericUpDownDetailsUnitStock.DecimalPlaces = 2;
                numericUpDownQuantity.DecimalPlaces = 2;

                //assign the minimal value and the maximum value to the Minimum and Maximum properties
                numericUpDownDetailsUnitStock.Maximum = maxUnit;
                numericUpDownDetailsUnitStock.Minimum = minimalUnit;

                //assign the row.Cells["unit_stock"] value to the numericUpDown, here we also need to convert it to the decimal first
                numericUpDownDetailsUnitStock.Value = Convert.ToDecimal(row.Cells["unit_stock"].Value);

                //numeric up down quantity minimum value, avoid user buys negative items (-1,-2, so on)
                numericUpDownQuantity.Minimum = 1;
            }
        }

        //when we press the buy button
        private void buttonBuy_Click(object sender, EventArgs e)
        {
            //numericUpDownQuantity logic
            //if the numeric up down value is higher than the unit stock numeric up down value
            if (numericUpDownQuantity.Value > numericUpDownDetailsUnitStock.Value)
            {
                labelError.Visible = true;
                labelError.Text = "Cannot calculate total when quantity higher than stock";
                return;
            }

            //total transaction formula
            decimal quantity = numericUpDownQuantity.Value;
            decimal price = numericUpDownPriceUnit.Value;
            decimal total = quantity * price;

            labelTotal.Text = total.ToString();

            //if there's no error
            labelError.Visible = false;
        }
    }
}
