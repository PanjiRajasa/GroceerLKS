using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceerLKS
{
    public class VendorProdList
    {
        DataClasses1DataContext db = new DataClasses1DataContext(); //connect to the database
        private IEnumerable<object> vendorProduct; //variable thah holds the vendor product data

        //constructor to load the data when the object loaded
        public VendorProdList()
        {
            LoadDatProducts();
        }

        //method to load the data
        private void LoadDatProducts()
        {
            vendorProduct = from p in db.products
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
        }

        //display and get the data
        public IEnumerable<object> GetProdList()
        {   
            //make sure that the product isn't null
            if(vendorProduct == null)
            {
                LoadDatProducts();
            }

            //return the product data
            return vendorProduct;
        }
    }
}
