namespace GroceerLKS
{
    partial class TransactionCustomer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dataGridViewHistory = new System.Windows.Forms.DataGridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dataGridViewPending = new System.Windows.Forms.DataGridView();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.labelDeliveryCost = new System.Windows.Forms.Label();
            this.labelTotalTransaction = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.labelPriceperUnit = new System.Windows.Forms.Label();
            this.labelQuantity = new System.Windows.Forms.Label();
            this.labelVendorName = new System.Windows.Forms.Label();
            this.labelProductName = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewHistory)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPending)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(329, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "Transaction";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dataGridViewHistory);
            this.groupBox1.Location = new System.Drawing.Point(25, 79);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(750, 135);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "History";
            // 
            // dataGridViewHistory
            // 
            this.dataGridViewHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewHistory.Location = new System.Drawing.Point(6, 19);
            this.dataGridViewHistory.Name = "dataGridViewHistory";
            this.dataGridViewHistory.Size = new System.Drawing.Size(738, 110);
            this.dataGridViewHistory.TabIndex = 0;
            this.dataGridViewHistory.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewHistory_CellClick);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dataGridViewPending);
            this.groupBox2.Location = new System.Drawing.Point(25, 220);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(750, 141);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Pending";
            // 
            // dataGridViewPending
            // 
            this.dataGridViewPending.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewPending.Location = new System.Drawing.Point(6, 19);
            this.dataGridViewPending.Name = "dataGridViewPending";
            this.dataGridViewPending.Size = new System.Drawing.Size(738, 116);
            this.dataGridViewPending.TabIndex = 0;
            this.dataGridViewPending.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewPending_CellClick);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.buttonCancel);
            this.groupBox3.Controls.Add(this.labelDeliveryCost);
            this.groupBox3.Controls.Add(this.labelTotalTransaction);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.labelPriceperUnit);
            this.groupBox3.Controls.Add(this.labelQuantity);
            this.groupBox3.Controls.Add(this.labelVendorName);
            this.groupBox3.Controls.Add(this.labelProductName);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Location = new System.Drawing.Point(160, 367);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(515, 180);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Details";
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(397, 28);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(97, 52);
            this.buttonCancel.TabIndex = 12;
            this.buttonCancel.Text = "Cancel Transaction";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // labelDeliveryCost
            // 
            this.labelDeliveryCost.AutoSize = true;
            this.labelDeliveryCost.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDeliveryCost.Location = new System.Drawing.Point(309, 107);
            this.labelDeliveryCost.Name = "labelDeliveryCost";
            this.labelDeliveryCost.Size = new System.Drawing.Size(32, 13);
            this.labelDeliveryCost.TabIndex = 11;
            this.labelDeliveryCost.Text = "0.00";
            // 
            // labelTotalTransaction
            // 
            this.labelTotalTransaction.AutoSize = true;
            this.labelTotalTransaction.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTotalTransaction.Location = new System.Drawing.Point(309, 80);
            this.labelTotalTransaction.Name = "labelTotalTransaction";
            this.labelTotalTransaction.Size = new System.Drawing.Size(32, 13);
            this.labelTotalTransaction.TabIndex = 10;
            this.labelTotalTransaction.Text = "0.00";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(213, 107);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(69, 13);
            this.label7.TabIndex = 9;
            this.label7.Text = "Delivery Cost";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(213, 80);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(90, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "Total Transaction";
            // 
            // labelPriceperUnit
            // 
            this.labelPriceperUnit.AutoSize = true;
            this.labelPriceperUnit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPriceperUnit.Location = new System.Drawing.Point(111, 107);
            this.labelPriceperUnit.Name = "labelPriceperUnit";
            this.labelPriceperUnit.Size = new System.Drawing.Size(32, 13);
            this.labelPriceperUnit.TabIndex = 7;
            this.labelPriceperUnit.Text = "0.00";
            // 
            // labelQuantity
            // 
            this.labelQuantity.AutoSize = true;
            this.labelQuantity.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelQuantity.Location = new System.Drawing.Point(111, 80);
            this.labelQuantity.Name = "labelQuantity";
            this.labelQuantity.Size = new System.Drawing.Size(32, 13);
            this.labelQuantity.TabIndex = 6;
            this.labelQuantity.Text = "0.00";
            // 
            // labelVendorName
            // 
            this.labelVendorName.AutoSize = true;
            this.labelVendorName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelVendorName.Location = new System.Drawing.Point(111, 53);
            this.labelVendorName.Name = "labelVendorName";
            this.labelVendorName.Size = new System.Drawing.Size(47, 13);
            this.labelVendorName.TabIndex = 5;
            this.labelVendorName.Text = "Vendor";
            // 
            // labelProductName
            // 
            this.labelProductName.AutoSize = true;
            this.labelProductName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelProductName.Location = new System.Drawing.Point(111, 28);
            this.labelProductName.Name = "labelProductName";
            this.labelProductName.Size = new System.Drawing.Size(51, 13);
            this.labelProductName.TabIndex = 4;
            this.labelProductName.Text = "Product";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 107);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "Price per Unit";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 80);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Quantity";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Vendor Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Product Name";
            // 
            // TransactionCustomer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 582);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Name = "TransactionCustomer";
            this.Text = "TransactionCustomer";
            this.Load += new System.EventHandler(this.TransactionCustomer_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewHistory)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPending)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label labelPriceperUnit;
        private System.Windows.Forms.Label labelQuantity;
        private System.Windows.Forms.Label labelVendorName;
        private System.Windows.Forms.Label labelProductName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridView dataGridViewHistory;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label labelDeliveryCost;
        private System.Windows.Forms.Label labelTotalTransaction;
        private System.Windows.Forms.DataGridView dataGridViewPending;
    }
}