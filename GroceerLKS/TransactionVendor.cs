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
    public partial class TransactionVendor: Form
    {
        public TransactionVendor()
        {
            InitializeComponent();
        }

        //data connection variable to store the data connection
        DataClasses1DataContext db = new DataClasses1DataContext();
        
        //when the form loaded
        private void TransactionVendor_Load(object sender, EventArgs e)
        {
            //disable the approve and decline button
            buttonApprove.Enabled = false;
            buttonDecline.Enabled = false;

            //to save the history transaction for vendor, here I will use join
            var history = (from transaction in db.transactions
                          join vendor in db.users on transaction.vendor_id equals vendor.id
                          join customer in db.users on transaction.customer_id equals customer.id
                          join product in db.products on transaction.product_id equals product.id
                          where
                          vendor.id == SessionManager.ID
                          && vendor.vendor_active == 1
                          && (transaction.status == "success" || transaction.status == "abort")
                          && (customer.cust_active == 0 || customer.id != vendor.id)
                          select new
                          {
                              transactionID = transaction.id,
                              product.product_name,
                              customer.cust_name,
                              transaction.quantity,
                              product.price_per_unit,
                              transaction.total_price,
                              transaction.delivery_cost,
                              transaction.status,
                              transaction.updated_at,
                              transaction.created_at
                          } ).ToList();


            //display the data
            dataGridViewHistory.DataSource = history;

            //to save the pending transaction for vendor, I use join too
            var pending = (from transaction in db.transactions
                           join vendor in db.users on transaction.vendor_id equals vendor.id
                           join customer in db.users on transaction.customer_id equals customer.id
                           join product in db.products on transaction.product_id equals product.id
                           where
                           vendor.id == SessionManager.ID
                           && vendor.vendor_active == 1
                           && (transaction.status == "pending")
                           && (customer.cust_active == 0 || customer.id != vendor.id)
                           select new
                           {
                               transactionID = transaction.id,
                               product.product_name,
                               vendor.vendor_name,
                               transaction.quantity,
                               product.price_per_unit,
                               transaction.total_price,
                               transaction.delivery_cost,
                               transaction.status,
                               transaction.updated_at,
                               transaction.created_at
                           }).ToList();

            //display the data
            dataGridViewPending.DataSource = pending;

            //hide the id
            dataGridViewPending.Columns["transactionID"].Visible = false;
            dataGridViewHistory.Columns["transactionID"].Visible = false;

        }

        public int TransactionID = -1; //public variable to save a transaction ID


        //event when we click the dataGridViewRow
        private void dataGridViewHistory_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //disable the approve and decline button
            buttonApprove.Enabled = false;
            buttonDecline.Enabled = false;


            //e variable contains the event arguments such as current selected row

            //avoid a minus index, minus index will return an error
            if (e.RowIndex >= 0)
            {
                //select the row with the DataGridViewRow object, and we will call the e.RowIndex rows
                DataGridViewRow Rows = dataGridViewHistory.Rows[e.RowIndex];

                //assign the value to the form components
                labelProduct.Text = Rows.Cells["product_name"].Value.ToString();
                labelCustomer.Text = Rows.Cells["cust_name"].Value.ToString();
                labelQuantity.Text = Rows.Cells["quantity"].Value.ToString();
                labelPricePerUnit.Text = Rows.Cells["price_per_unit"].Value.ToString();
                labelTotalTransaction.Text = Rows.Cells["total_price"].Value.ToString();
                labelDeliveryCost.Text = Rows.Cells["delivery_cost"].Value.ToString();

                //assign the id
                TransactionID = Convert.ToInt32(Rows.Cells["transactionID"].Value);
            }
        }

        private void dataGridViewPending_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //disable the approve and decline button
            buttonApprove.Enabled = true;
            buttonDecline.Enabled = true;

            //e variable contains the event arguments such as current selected row

            //avoid a minus index, minus index will return an error
            if (e.RowIndex >= 0)
            {
                //select the row with the DataGridViewRow object, and we will call the e.RowIndex rows
                DataGridViewRow Rows = dataGridViewPending.Rows[e.RowIndex];

                //assign the value to the form components
                labelProduct.Text = Rows.Cells["product_name"].Value.ToString();
                labelCustomer.Text = Rows.Cells["vendor_name"].Value.ToString();
                labelQuantity.Text = Rows.Cells["quantity"].Value.ToString();
                labelPricePerUnit.Text = Rows.Cells["price_per_unit"].Value.ToString();
                labelTotalTransaction.Text = Rows.Cells["total_price"].Value.ToString();
                labelDeliveryCost.Text = Rows.Cells["delivery_cost"].Value.ToString();

                //assign the id
                TransactionID = Convert.ToInt32(Rows.Cells["transactionID"].Value);
            }
        }

        //when user approve the transaction
        private void buttonApprove_Click(object sender, EventArgs e)
        {
            var selectedTransaction = (from transaction in db.transactions where transaction.id == TransactionID select transaction).FirstOrDefault();

            //if the transaction does not null
            if(selectedTransaction != null)
            {
                selectedTransaction.status = "success"; //change to success
                //use try catch to avoid bugs when submit
                try 
                {
                    db.SubmitChanges();
                    RefreshUI();
                    //reset the transaction ID
                    TransactionID = -1;
                }
                catch (Exception ex)
                {
                    //refresh the db if there's an error
                    db.Refresh(System.Data.Linq.RefreshMode.OverwriteCurrentValues, db.transactions);
                    //refresh the UI too
                    RefreshUI();
                    //catch the error
                    Console.WriteLine($"\n\n {ex.Message} \n\n");
                }
            }
        }

        //when the user cancel the transaction
        private void buttonDecline_Click(object sender, EventArgs e)
        {
            var selectedTransaction = (from transaction in db.transactions where transaction.id == TransactionID select transaction).FirstOrDefault();

            //if the transaction does not null
            if (selectedTransaction != null)
            {
                selectedTransaction.status = "abort"; //change to abort

                //product that related to the transaction, here we will select the quantity then we will use it for the restock logic
                var relatedProduct = (from product in db.products
                                      where product.id == selectedTransaction.product_id
                                      select product).FirstOrDefault();

                //if the product founded
                if(relatedProduct != null)
                {
                    //add the selectedTransaction.quantity to the related product
                    relatedProduct.unit_stock += selectedTransaction.quantity;
                }

                //use try catch to avoid bugs when submit
                try
                {
                    db.SubmitChanges();
                    RefreshUI();
                    //reset the transaction ID
                    TransactionID = -1;
                }
                catch (Exception ex)
                {
                    //refresh the db if there's an error
                    db.Refresh(System.Data.Linq.RefreshMode.OverwriteCurrentValues, db.transactions);
                    //refresh the UI too
                    RefreshUI();
                    //catch the error
                    Console.WriteLine($"\n\n {ex.Message} \n\n");
                }
            }

        }

        //to load the data after do a change
        private void RefreshUI()
        {
            //to save the history transaction for vendor, here I will use join
            var history = (from transaction in db.transactions
                           join vendor in db.users on transaction.vendor_id equals vendor.id
                           join customer in db.users on transaction.customer_id equals customer.id
                           join product in db.products on transaction.product_id equals product.id
                           where
                           vendor.id == SessionManager.ID
                           && vendor.vendor_active == 1
                           && (transaction.status == "success" || transaction.status == "abort")
                           && (customer.cust_active == 0 || customer.id != vendor.id)
                           select new
                           {
                               transactionID = transaction.id,
                               product.product_name,
                               customer.cust_name,
                               transaction.quantity,
                               product.price_per_unit,
                               transaction.total_price,
                               transaction.delivery_cost,
                               transaction.status,
                               transaction.updated_at,
                               transaction.created_at
                           }).ToList();


            //display the data
            dataGridViewHistory.DataSource = history;

            //to save the pending transaction for vendor, I use join too
            var pending = (from transaction in db.transactions
                           join vendor in db.users on transaction.vendor_id equals vendor.id
                           join customer in db.users on transaction.customer_id equals customer.id
                           join product in db.products on transaction.product_id equals product.id
                           where
                           vendor.id == SessionManager.ID
                           && vendor.vendor_active == 1
                           && (transaction.status == "pending")
                           && (customer.cust_active == 0 || customer.id != vendor.id)
                           select new
                           {
                               transactionID = transaction.id,
                               product.product_name,
                               vendor.vendor_name,
                               transaction.quantity,
                               product.price_per_unit,
                               transaction.total_price,
                               transaction.delivery_cost,
                               transaction.status,
                               transaction.updated_at,
                               transaction.created_at
                           }).ToList();

            //display the data
            dataGridViewPending.DataSource = pending;

        }
    }
}
