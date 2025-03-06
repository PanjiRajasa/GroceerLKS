using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceerLKS
{   
    //to store the active product
    public class ActiveProduct
    {
        private DataClasses1DataContext db = new DataClasses1DataContext(); //data context -> connect to the database
        private IEnumerable<object> activeProduct; //variable to store the active product data, we are using IEnumerable<object> data type

        //constructor to load the data when the object is created
        public ActiveProduct()
        {
            LoadActiveProducts();
        }

        private void LoadActiveProducts()
        {
            //load the data

            //dataGridView data
            //Here I use the join query to combine product + user + categories with some selections

            //from the products table, join it with the users table on vendor id from the products table equal to the id from the user, then join it with the categories table on category id from the products table equal to the user id from the users table
            //where (criteria that indicate whether the products is active or not, is_active from the products table == 1, unit_stock from the products table more than 0 -> unit_stock > 0, and the last, vendor_active status == 1 from the users table
            //then we will display a new object which contains vendor_name,product_name,name (category), unit_type, price_per_unit, unit_stock
            activeProduct = from p in db.products
                            join v in db.users on p.vendor_id equals v.id
                            join c in db.categories on p.category_id equals c.id
                            where p.is_active == 1
                            && p.unit_stock > 0
                            && v.vendor_active == 1
                            && v.id != SessionManager.ID //if we login as customer that also has an active vendor role, our products won't be displayed
                            //customize what we will display to the dataGridView
                            select new
                            {
                                productID = p.id,
                                vendorID = v.id,
                                v.vendor_name,
                                p.product_name,
                                category_name = c.name,
                                p.unit_type,
                                p.price_per_unit,
                                p.unit_stock,
                                v.vendor_latitude, //this column will be hidden, because we don not need it to be displayed, but we neeed it for the delivery total logic
                                v.vendor_longitude //this column will be hidden too
                            };
        }

        //method to call the LoadActiveProducts() method
        public IEnumerable<object> GetActiveProducts()
        {   
            //if the activeProduct is null, then we will call the constructor to avoid null dat
            if(activeProduct == null)
            {
                LoadActiveProducts();
            }

            return activeProduct;
        }
    }
}
