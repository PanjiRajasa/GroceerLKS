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

            //hide the error label by default
            labelError.Visible = false;

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
    }
}
