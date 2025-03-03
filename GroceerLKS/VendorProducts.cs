using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace GroceerLKS
{
    public partial class VendorProducts: Form
    {
        public VendorProducts()
        {
            InitializeComponent();
        }

        //data context
        DataClasses1DataContext db = new DataClasses1DataContext();

        //when the form is loaded
        private void VendorProducts_Load(object sender, EventArgs e)
        {
            //default configuration

            //hide the label error
            labelError.Visible = false;
            //disabled the add item and the delete button 
            buttonDelete.Enabled = false;
            buttonEdit.Enabled = false;

            //load the dataGridView data

            //make an instance
            //VendorProdList vendorProdList = new VendorProdList();
            //call the method and then save it inside a variable
            //var vendorProduct = vendorProdList.GetProdList();
            var vendorProduct = from p in db.products
                                join u in db.users on p.vendor_id equals u.id
                                join c in db.categories on p.category_id equals c.id
                                where p.deleted_at == null
                                && u.id == SessionManager.ID
                                select new
                                {
                                    p.category_id,
                                    productID = p.id,
                                    p.product_name,
                                    category_name = c.name,
                                    p.unit_type,
                                    p.price_per_unit,
                                    p.unit_stock,
                                    status = p.is_active == 1 //short to boolean, so we can use checkbox inside the dataGridView to represent this column, this way is enough, because if there's a boolean data, the items will be automatically become a checkBox
                                };

            //then display the data
            dataGridViewVendor.DataSource = vendorProduct.ToList();

            //hide the id data
            dataGridViewVendor.Columns["category_id"].Visible = false;
            dataGridViewVendor.Columns["productID"].Visible = false;
        }

        //category id
        public int CategoryID { get; private set; }

        //unit type
        public string UnitType { get; private set; }

        //product ID
        private int productID = -1; //-1 berarti tidak ada yang pilih

        //when the cells clicked
        private void dataGridViewVendor_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //show the disabled buttons
            buttonDelete.Enabled = true;
            buttonEdit.Enabled = true;

            //avoid a negative column index (0 is not included as negative index because index starts from 0)
            if(e.RowIndex >= 0)
            {
                //call the DataGridViewRow object to select the clicked row
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
                DataGridViewRow dataGridViewRow = dataGridViewVendor.Rows[e.RowIndex];

                //assign the data to the form components, here we will use dataGridViewRow.Cells["name"].Value

                //set the productID
                productID = Convert.ToInt32(dataGridViewRow.Cells["productID"].Value);

                //name
                textBoxName.Text = dataGridViewRow.Cells["product_name"].Value.ToString();

                //radio button
                bool unitType = dataGridViewRow.Cells["unit_type"].Value.ToString() == "measurable" ? true : false;
                //use if statement
                if (unitType) radioButtonMeasurable.Checked = true;
                else radioButtonCountable.Checked = true;

                //category comboBox
                //assign to the comboBox
                comboBoxCategory.DataSource = db.categories.Where(c => c.is_active == 1).Select(c => c.name).ToList();
                //item that currently displayed, we will use from the DB
                comboBoxCategory.SelectedItem = dataGridViewRow.Cells["category_name"].Value;

                //comboBox status
                //first we need fill it with the fix options which is active and inactive
                //active -> 1, inactive -> 0
                comboBoxStatus.DataSource = new List<string> { "Active", "Inactive" };

                //make sure that user select an option
                if (dataGridViewRow != null)
                {
                    //collect status value from the dataGridView (0 or 1)
                    bool isActive = Convert.ToBoolean(dataGridViewRow.Cells["status"].Value); //select status value, then convert it to the boolean,because here we will make the comboBox works like a switch which can be switched to active or inactive
                    //make the condition
                    comboBoxStatus.SelectedItem = isActive ? "Active" : "Inactive";
                }

                //numericUpDownPrice

                //set the maximum and the minimum value 
                numericUpDownPrice.Minimum = 0;
                numericUpDownPrice.Maximum = decimal.MaxValue;

                numericUpDownPrice.Value = Convert.ToDecimal(dataGridViewRow.Cells["price_per_unit"].Value);

                //set the maximum and the minimum value 
                numericUpDownStock.Minimum = 0;
                //so the value will be infinity
                numericUpDownStock.Maximum = decimal.MaxValue;
                //to allow decimal value
                numericUpDownStock.DecimalPlaces = 2;

                //numericUpDownStock
                numericUpDownStock.Value = Convert.ToDecimal(dataGridViewRow.Cells["unit_stock"].Value);

                //set the public category id value
                CategoryID = int.Parse(dataGridViewRow.Cells["category_id"].Value.ToString());

                //set the unit type value
                string unitTypeVal = dataGridViewRow.Cells["unit_type"].Value.ToString();

                if (radioButtonCountable.Checked) unitTypeVal = "countable";
                if (radioButtonMeasurable.Checked) unitTypeVal = "measurable";

                UnitType = unitTypeVal;
            }
        }

        //when the user press the cancel button, they will back to main form
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            //cancel logic
            try
            {
                db.Refresh(System.Data.Linq.RefreshMode.OverwriteCurrentValues, db.products); //refresh DB
                RefreshUI(); //refresh UI

                //debug
                MessageBox.Show("success cancel");
                Console.WriteLine("Success cancel");
            } catch (Exception ex)
            {   
                //debug
                MessageBox.Show("" + ex);
                Console.WriteLine(ex);
            }
            

        }

        //save to the database if we clicked this button
        private void buttonSave_Click(object sender, EventArgs e)
        {
            //to avoid System.Data.Linq.ChangeConflictException bugs
            try
            {
                
                db.SubmitChanges(); // we try to save the data
                db.Refresh(System.Data.Linq.RefreshMode.OverwriteCurrentValues, db.products); //refresh 
                //make everything empty
                ClearFormInputs();

                //succesfully message for debug
                MessageBox.Show("Semua perubahan berhasil disimpan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                //messagebox for debug
                MessageBox.Show("Terjadi kesalahan saat menyimpan: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine(ex);
                //if error occurs, then we will refresh the data
                db.Refresh(System.Data.Linq.RefreshMode.OverwriteCurrentValues, db.products);
                db.SubmitChanges();
                //make everything empty
                ClearFormInputs();
            }

            //refresh the UI
            RefreshUI();
        }

        //refresh UI method
        private void RefreshUI()
        {
            var vendorProducts = from p in db.products
                                 join u in db.users on p.vendor_id equals u.id
                                 join c in db.categories on p.category_id equals c.id
                                 where p.deleted_at == null
                                 && u.id == SessionManager.ID
                                 select new
                                 {
                                     p.category_id,
                                     productID = p.id,
                                     p.product_name,
                                     category_name = c.name,
                                     p.unit_type,
                                     p.price_per_unit,
                                     p.unit_stock,
                                     status = p.is_active == 1 //short to boolean, so we can use checkbox inside the dataGridView to represent this column, this way is enough, because if there's a boolean data, the items will be automatically become a checkBox
                                 };

            comboBoxCategory.DataSource = db.categories.Where(c => c.is_active == 1).Select(c => c.name).ToList(); //update comboBox
            comboBoxCategory.SelectedIndex = -1; // Reset category
            dataGridViewVendor.DataSource = vendorProducts.ToList();
        }

        //clear form input
        private void ClearFormInputs()
        {
            //make everything empty
            textBoxName.Text = null;
            comboBoxCategory.SelectedItem = null;
            radioButtonCountable.Checked = false;
            radioButtonMeasurable.Checked = false;
            comboBoxStatus.SelectedItem = null;
            numericUpDownPrice.Value = numericUpDownPrice.Minimum;
            numericUpDownStock.Value = numericUpDownStock.Minimum;
        }

        //to add the new item to the db
        private void buttonAddItem_Click(object sender, EventArgs e)
        {
            //all column must be filled, validation, I use || operator this time
            if (Utils.isEmptyWhiteSpaceString(textBoxName.Text)
                || Utils.isEmptyWhiteSpaceString(comboBoxCategory.Text)
                || Utils.isEmptyWhiteSpaceString(comboBoxStatus.Text)
                || (!radioButtonCountable.Checked && !radioButtonMeasurable.Checked))
            {
                labelError.Visible = true;
                labelError.Text = "All fields must be filled!";
                return;
            }

            //category must be binded from the db, and must be the active category
            if(!db.categories.Where(c => c.is_active == 1).Select(c => c.name).Contains(comboBoxCategory.Text))
            {
                labelError.Visible = true;
                labelError.Text = "Category must be category that exist in db!";
                return;
            }

            //price must be digit and can't be 0 or a negative value
            if(numericUpDownPrice.Value <= 0 || !Utils.isDigit(numericUpDownPrice.Value.ToString() ) )
            {
                labelError.Visible = true;
                labelError.Text = "Price value must be digit!";
                return;
            }

            //stock can't be 0 or a negative value
            if(numericUpDownStock.Value <= 0)
            {
                labelError.Visible = true;
                labelError.Text = "Stock must be higher than 0";
                return;
            }

            // countable stock should be integer
            //value % 1 == 0 -> integer, for example 5 % 1 = 0 because 1 * 5 = 5
            //value % 1 != 0 -> float, for example 20.5 % 1 = 5 because 1 * 20 + 0.5
            //if the countable radio button checked but the value is a float
            if(radioButtonCountable.Checked && numericUpDownStock.Value % 1 != 0)
            {
                labelError.Visible = true;
                labelError.Text = "Countable unit type stock must have rounded value";
                return;
            }

            // measurable should be able to input float data type -> we don't need to be validated, because if we insert 2000 (int) or (2000.0) it will be counted as float, it possible because numericUpDownStock.Value = Convert.ToDecimal(dataGridViewRow.Cells["unit_stock"].Value); -> here we already convert it to the decimal

            //if there's no error
            labelError.Visible = false;

            //add to the DB

            //new product data
            var product = new product();

            //if the product isn't null
            if(product != null)
            {   
                //id, here I use orderByDescending with firstOrDefault to get the last ID
                product.id = (db.products.OrderByDescending(p => p.id).Select(p => p.id)).FirstOrDefault() + 1;

                //vendor ID, select ID that equivalent with the id from the login data
                product.vendor_id = (from u in db.users where u.id == SessionManager.ID select u.id).FirstOrDefault();

                //product name
                product.product_name = textBoxName.Text;

                //category id
                product.category_id = (from p in db.products
                                      join c in db.categories on p.category_id equals c.id
                                      where comboBoxCategory.SelectedItem.ToString() == c.name
                                      select c.id).FirstOrDefault();
                //for debug
                //MessageBox.Show("Perubahan pada " + product.category_id); 

                //unit type
                product.unit_type = UnitType;

                //price per unit
                product.price_per_unit = numericUpDownPrice.Value;

                //unit stock
                product.unit_stock = (double)numericUpDownStock.Value;

                //set status

                product.is_active = (short)(comboBoxStatus.Text == "active" ? 1 : 0);

                //TimeStamps
                product.created_at = DateTime.Now;
                product.updated_at = DateTime.Now;

                //insert the changes
                db.products.InsertOnSubmit(product);
            }
        }

        //logic to edit the button
        private void buttonEdit_Click(object sender, EventArgs e)
        {
            //all column must be filled, validation, I use || operator this time
            if (Utils.isEmptyWhiteSpaceString(textBoxName.Text)
                || Utils.isEmptyWhiteSpaceString(comboBoxCategory.Text)
                || Utils.isEmptyWhiteSpaceString(comboBoxStatus.Text)
                || (!radioButtonCountable.Checked && !radioButtonMeasurable.Checked))
            {
                labelError.Visible = true;
                labelError.Text = "All fields must be filled!";
                return;
            }

            //category must be binded from the db, and must be the active category
            if (!db.categories.Where(c => c.is_active == 1).Select(c => c.name).Contains(comboBoxCategory.Text))
            {
                labelError.Visible = true;
                labelError.Text = "Category must be category that exist in db!";
                return;
            }

            //price must be digit and can't be 0 or a negative value
            if (numericUpDownPrice.Value <= 0 || !Utils.isDigit(numericUpDownPrice.Value.ToString()))
            {
                labelError.Visible = true;
                labelError.Text = "Price value must be digit and must be higher than 0!";
                return;
            }

            //stock can't be 0 or a negative value
            if (numericUpDownStock.Value <= 0)
            {
                labelError.Visible = true;
                labelError.Text = "Stock must be higher than 0";
                return;
            }

            // countable stock should be integer
            //value % 1 == 0 -> integer, for example 5 % 1 = 0 because 1 * 5 = 5
            //value % 1 != 0 -> float, for example 20.5 % 1 = 5 because 1 * 20 + 0.5
            //if the countable radio button checked but the value is a float
            if (radioButtonCountable.Checked && numericUpDownStock.Value % 1 != 0)
            {
                labelError.Visible = true;
                labelError.Text = "Countable unit type stock must have rounded value";
                return;
            }

            // measurable should be able to input float data type -> we don't need to be validated, because if we insert 2000 (int) or (2000.0) it will be counted as float, it possible because numericUpDownStock.Value = Convert.ToDecimal(dataGridViewRow.Cells["unit_stock"].Value); -> here we already convert it to the decimal

            //if there's no error
            labelError.Visible = false;

            //add to the DB

            //new product data
            var product = db.products.Where(s => s.id == productID).Select(s => s).FirstOrDefault();

            //if the product isn't null
            if (product != null)
            {
                //vendor ID, select ID that equivalent with the id from the login data
                product.vendor_id = (from u in db.users where u.id == SessionManager.ID select u.id).FirstOrDefault();

                //product name
                product.product_name = textBoxName.Text;

                //category id
                product.category_id = (from p in db.products
                                       join c in db.categories on p.category_id equals c.id
                                       where comboBoxCategory.SelectedItem.ToString() == c.name
                                       select c.id).FirstOrDefault();
                //for debug
                //MessageBox.Show("Perubahan pada " + product.category_id);

                //unit type
                product.unit_type = UnitType;

                //price per unit
                product.price_per_unit = numericUpDownPrice.Value;

                //unit stock
                product.unit_stock = (double)numericUpDownStock.Value;

                //set status
                //we use Equals function to compare, is comboBoxStatus.Text have a value that == "Active" ? if yes, it will return 1, if not, it will return 0, 1 -> active, 0 -> inactive. Then to make it case insensitive we use  StringComparison.OrdinalIgnoreCase, StringComparison.OrdinalIgnoreCase will make the comparison logic is not based on the case sensitive anymore.
                product.is_active = (short)(comboBoxStatus.Text.Equals("Active", StringComparison.OrdinalIgnoreCase) ? 1 : 0);

                //TimeStamps
                product.created_at = DateTime.Now;
                product.updated_at = DateTime.Now;

                //Reset productID
                productID = -1; //-1 == empty
            }
        }

        //logic to delete the item
        private void buttonDelete_Click(object sender, EventArgs e)
        {   
            //for debugging
            if(productID == -1)
            {
                MessageBox.Show("select product first!");
                return;
            }

            //use try catch to avoid error
            try
            {
                var selectedProduct = (from p in db.products where p.id == productID select p).FirstOrDefault();

                if(selectedProduct != null)
                {
                    selectedProduct.deleted_at = DateTime.Now;
                    //debug
                    MessageBox.Show("Success Delete");
                }

            } catch (Exception ex)
            {
                //for debug
                MessageBox.Show("Error occurs: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //if there's no error
            labelError.Visible = false;

        }
    }
}
