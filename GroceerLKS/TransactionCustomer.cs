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
    public partial class TransactionCustomer: Form
    {
        DataClasses1DataContext db = new DataClasses1DataContext(); //data connection

        public TransactionCustomer()
        {
            InitializeComponent();
        }

        //when the form loaded
        private void TransactionCustomer_Load(object sender, EventArgs e)
        {
            //history transaction
            var history = from s in db.transactions where s.customer_id == SessionManager.ID && (s.status == "success" || s.status == "abort")  select s;
            //display the data
            dataGridViewHistory.DataSource = history;


            //pending transaction
            var pendingTransaction = (from u in db.transactions where u.customer_id == SessionManager.ID && u.status == "pending" select u);
            //display the data
            dataGridViewPending.DataSource = pendingTransaction;
        }
    }
}
