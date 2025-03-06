using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GroceerLKS
{
    public partial class TransactionCustomer : Form
    {
        DataClasses1DataContext db = new DataClasses1DataContext(); //data connection

        public TransactionCustomer()
        {
            InitializeComponent();
        }

        //public selectedTansaction
        public string statusTransaction { get; private set; }
        //public transaction id
        private int transactionID = -1;

        //when the form loaded
        private void TransactionCustomer_Load(object sender, EventArgs e)
        {
            //disable the cancel button by default
            buttonCancel.Enabled = false;

            //history transaction
            var history = (from t in db.transactions
                           join p in db.products on t.product_id equals p.id
                           join c in db.users on t.customer_id equals c.id
                           join v in db.users on t.vendor_id equals v.id
                           where
                           c.id == SessionManager.ID
                           && c.cust_active == 1
                           && (t.status == "success" || t.status == "abort")
                           && (c.vendor_active == 0 || p.vendor_id != c.id)
                           select new
                           {
                               p.product_name,
                               v.vendor_name,
                               t.quantity,
                               p.price_per_unit,
                               t.total_price,
                               t.delivery_cost,
                               t.status,
                               t.updated_at,
                               t.created_at
                           }).ToList();

            //display the data
            dataGridViewHistory.DataSource = history;

            //pending transaction
            var pendingTransaction = (from transaction in db.transactions
                                      join product in db.products on transaction.product_id equals product.id
                                      join customer in db.users on transaction.customer_id equals customer.id
                                      join vendor in db.users on transaction.vendor_id equals vendor.id
                                      where
                                      customer.id == SessionManager.ID
                                      && customer.cust_active == 1
                                      && (transaction.status == "pending")
                                      && (customer.vendor_active == 0 || product.vendor_id != customer.id)
                                      select new
                                      {
                                          transaction_id = transaction.id,
                                          product.product_name,
                                          vendor.vendor_name,
                                          transaction.quantity,
                                          product.price_per_unit,
                                          transaction.total_price,
                                          transaction.delivery_cost,
                                          transaction.status,
                                          transaction.updated_at,
                                          transaction.created_at
                                      }
                                      ).ToList();
            //display the data
            dataGridViewPending.DataSource = pendingTransaction;

            //hide the id
            dataGridViewPending.Columns["transaction_id"].Visible = false;
        }

        //when click the data cell

        //history data grid view
        private void dataGridViewHistory_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //disable the cancel button
            buttonCancel.Enabled = false;

            //avoid a negative index
            if (e.RowIndex >= 0)
            {
                //select the clicked row
                DataGridViewRow dataGridViewRow = dataGridViewHistory.Rows[e.RowIndex];

                //display the data
                labelProductName.Text = dataGridViewRow.Cells["product_name"].Value.ToString();
                labelVendorName.Text = dataGridViewRow.Cells["vendor_name"].Value.ToString();
                labelQuantity.Text = (Convert.ToDecimal(dataGridViewRow.Cells["quantity"].Value)).ToString();
                labelPriceperUnit.Text = (Convert.ToDecimal(dataGridViewRow.Cells["price_per_unit"].Value)).ToString();
                labelTotalTransaction.Text = (Convert.ToDecimal(dataGridViewRow.Cells["total_price"].Value)).ToString();
                labelDeliveryCost.Text = (Convert.ToDecimal(dataGridViewRow.Cells["delivery_cost"].Value).ToString());
            }

        }

        //data grid view pending
        private void dataGridViewPending_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //enable the cancel button
            buttonCancel.Enabled = true;

            //avoid a negative index
            if (e.RowIndex >= 0)
            {
                //select the clicked row
                DataGridViewRow dataGridViewRow = dataGridViewPending.Rows[e.RowIndex];

                //display the data
                labelProductName.Text = dataGridViewRow.Cells["product_name"].Value.ToString();
                labelVendorName.Text = dataGridViewRow.Cells["vendor_name"].Value.ToString();
                labelQuantity.Text = (Convert.ToDecimal(dataGridViewRow.Cells["quantity"].Value)).ToString();
                labelPriceperUnit.Text = (Convert.ToDecimal(dataGridViewRow.Cells["price_per_unit"].Value)).ToString();
                labelTotalTransaction.Text = (Convert.ToDecimal(dataGridViewRow.Cells["total_price"].Value)).ToString();
                labelDeliveryCost.Text = (Convert.ToDecimal(dataGridViewRow.Cells["delivery_cost"].Value).ToString());

                //public status transaction set
                statusTransaction = dataGridViewRow.Cells["status"].Value.ToString();
                //public transaction id set
                transactionID = Convert.ToInt32(dataGridViewRow.Cells["transaction_id"].Value);
            }
        }

        //refresh the UI
        private void RefreshUI()
        {
            //pending transaction
            var pendingTransaction = (from transaction in db.transactions
                                      join product in db.products on transaction.product_id equals product.id
                                      join customer in db.users on transaction.customer_id equals customer.id
                                      join vendor in db.users on transaction.vendor_id equals vendor.id
                                      where
                                      customer.id == SessionManager.ID
                                      && customer.cust_active == 1
                                      && (transaction.status == "pending")
                                      && (customer.vendor_active == 0 || product.vendor_id != customer.id)
                                      select new
                                      {
                                          transaction_id = transaction.id,
                                          product.product_name,
                                          vendor.vendor_name,
                                          transaction.quantity,
                                          product.price_per_unit,
                                          transaction.total_price,
                                          transaction.delivery_cost,
                                          transaction.status,
                                          transaction.updated_at,
                                          transaction.created_at
                                      }
                                      ).ToList();
            //display the data
            dataGridViewPending.DataSource = pendingTransaction;

            //history transaction
            var history = (from t in db.transactions
                           join p in db.products on t.product_id equals p.id
                           join c in db.users on t.customer_id equals c.id
                           join v in db.users on t.vendor_id equals v.id
                           where
                           c.id == SessionManager.ID
                           && c.cust_active == 1
                           && (t.status == "success" || t.status == "abort")
                           && (c.vendor_active == 0 || p.vendor_id != c.id)
                           select new
                           {
                               p.product_name,
                               v.vendor_name,
                               t.quantity,
                               p.price_per_unit,
                               t.total_price,
                               t.delivery_cost,
                               t.status,
                               t.updated_at,
                               t.created_at
                           }).ToList();

            //display the data
            dataGridViewHistory.DataSource = history;
        }

        //when the user cancel the transaction
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            //selectedTansaction variable
            var selectedTansaction = (from transaction in db.transactions
                          where transaction.id == transactionID
                          select transaction).FirstOrDefault();

            //if the selectedTansaction var is not null
            if (selectedTansaction != null)
            {
                //cancel the transaction
                selectedTansaction.status = "abort"; //change the status to abort

                //product that related to the transaction, here we will select the quantity then we will use it for the restock logic
                var relatedProduct = (from product in db.products
                                      where product.id == selectedTansaction.product_id
                                      select product).FirstOrDefault();

                //if the related product is not null
                if(relatedProduct != null)
                {
                    //add the cancelled related product's quantity back to the product 
                    relatedProduct.unit_stock += selectedTansaction.quantity;
                }
            }

            //submit the changes and avoid the error
            try
            {
                db.SubmitChanges();
                RefreshUI();
                transactionID = -1;
            } catch (Exception ex)
            {
                db.Refresh(System.Data.Linq.RefreshMode.OverwriteCurrentValues, db.transactions);
                db.SubmitChanges();
                RefreshUI();
                Console.WriteLine("The error is: " + ex.Message);
            }
        }
    }
}
