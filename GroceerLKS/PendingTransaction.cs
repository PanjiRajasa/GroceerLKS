using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceerLKS
{
    public class PendingTransaction
    {
        DataClasses1DataContext db = new DataClasses1DataContext(); //connection to the database with the DataContext
        private int pendingTransaction; //variable to hold the pending transaction

        //constructor to load the data when the object is called
        public PendingTransaction()
        {
            LoadData();
        }

        //method to load the data
        private void LoadData()
        {
            pendingTransaction = (from t in db.transactions
                                  join u in db.users on t.customer_id equals u.id
                                  where t.customer_id == SessionManager.ID
                                  && t.status == "pending"
                                  && u.cust_active == 1
                                  select t).Count();
        }

        //method to display the data
        public int GetPendingTransaction()
        {
            LoadData();
            return pendingTransaction;
        }
    }
}
