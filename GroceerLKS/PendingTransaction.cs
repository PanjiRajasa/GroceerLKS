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
        private int pendingTransaction; //variable to save the pending transaction

        //constructor to load the data when the object is called
        public PendingTransaction()
        {
            LoadData();
        }

        //method to load the data
        private void LoadData()
        {
            //We join the transactions table with the users table with the condition on customer ID from the transaction equals user's ID, then we filter it with the condition where customerIDd == SessionManager.ID, the transaction status must be "pending", then the user's current role must be a customer which is cust_active == 1. Then we will select the transaction result, then we will count it using the Count() method
            pendingTransaction = (from t in db.transactions //-> trancation table
                                  join u in db.users on t.customer_id equals u.id //-> user's table
                                  where t.customer_id == SessionManager.ID
                                  && t.status == "pending"
                                  && u.cust_active == 1 //active customer role
                                  select t).Count(); //count the result
        }

        //method to display the data
        public int GetPendingTransaction()
        {
            LoadData();
            return pendingTransaction;
        }
    }
}
