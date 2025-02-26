namespace GroceerLKS
{
    partial class CustomerProducts
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
            this.groupBoxList = new System.Windows.Forms.GroupBox();
            this.dataGridViewProducts = new System.Windows.Forms.DataGridView();
            this.labelError = new System.Windows.Forms.Label();
            this.groupBoxDetails = new System.Windows.Forms.GroupBox();
            this.numericUpDownDetailsUnitStock = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownPriceUnit = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.radioButtonMeasurable = new System.Windows.Forms.RadioButton();
            this.radioButtonCountable = new System.Windows.Forms.RadioButton();
            this.label7 = new System.Windows.Forms.Label();
            this.comboBoxCategoryProducts = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxProductName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBoxTransaction = new System.Windows.Forms.GroupBox();
            this.buttonClear = new System.Windows.Forms.Button();
            this.buttonBuy = new System.Windows.Forms.Button();
            this.labelDeliveryCost = new System.Windows.Forms.Label();
            this.labelTotal = new System.Windows.Forms.Label();
            this.numericUpDownQuantity = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBoxList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewProducts)).BeginInit();
            this.groupBoxDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDetailsUnitStock)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPriceUnit)).BeginInit();
            this.groupBoxTransaction.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownQuantity)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(320, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(149, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Product Area";
            // 
            // groupBoxList
            // 
            this.groupBoxList.Controls.Add(this.dataGridViewProducts);
            this.groupBoxList.Location = new System.Drawing.Point(12, 84);
            this.groupBoxList.Name = "groupBoxList";
            this.groupBoxList.Size = new System.Drawing.Size(776, 215);
            this.groupBoxList.TabIndex = 1;
            this.groupBoxList.TabStop = false;
            this.groupBoxList.Text = "List";
            // 
            // dataGridViewProducts
            // 
            this.dataGridViewProducts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewProducts.Location = new System.Drawing.Point(6, 19);
            this.dataGridViewProducts.Name = "dataGridViewProducts";
            this.dataGridViewProducts.Size = new System.Drawing.Size(764, 190);
            this.dataGridViewProducts.TabIndex = 0;
            this.dataGridViewProducts.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewProducts_CellClick);
            // 
            // labelError
            // 
            this.labelError.AutoSize = true;
            this.labelError.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelError.ForeColor = System.Drawing.Color.Red;
            this.labelError.Location = new System.Drawing.Point(249, 351);
            this.labelError.Name = "labelError";
            this.labelError.Size = new System.Drawing.Size(36, 16);
            this.labelError.TabIndex = 2;
            this.labelError.Text = "Error";
            // 
            // groupBoxDetails
            // 
            this.groupBoxDetails.Controls.Add(this.numericUpDownDetailsUnitStock);
            this.groupBoxDetails.Controls.Add(this.numericUpDownPriceUnit);
            this.groupBoxDetails.Controls.Add(this.label9);
            this.groupBoxDetails.Controls.Add(this.label8);
            this.groupBoxDetails.Controls.Add(this.radioButtonMeasurable);
            this.groupBoxDetails.Controls.Add(this.radioButtonCountable);
            this.groupBoxDetails.Controls.Add(this.label7);
            this.groupBoxDetails.Controls.Add(this.comboBoxCategoryProducts);
            this.groupBoxDetails.Controls.Add(this.label6);
            this.groupBoxDetails.Controls.Add(this.textBoxProductName);
            this.groupBoxDetails.Controls.Add(this.label5);
            this.groupBoxDetails.Location = new System.Drawing.Point(12, 385);
            this.groupBoxDetails.Name = "groupBoxDetails";
            this.groupBoxDetails.Size = new System.Drawing.Size(433, 176);
            this.groupBoxDetails.TabIndex = 3;
            this.groupBoxDetails.TabStop = false;
            this.groupBoxDetails.Text = "Details";
            // 
            // numericUpDownDetailsUnitStock
            // 
            this.numericUpDownDetailsUnitStock.Location = new System.Drawing.Point(313, 59);
            this.numericUpDownDetailsUnitStock.Name = "numericUpDownDetailsUnitStock";
            this.numericUpDownDetailsUnitStock.Size = new System.Drawing.Size(87, 20);
            this.numericUpDownDetailsUnitStock.TabIndex = 10;
            // 
            // numericUpDownPriceUnit
            // 
            this.numericUpDownPriceUnit.Location = new System.Drawing.Point(313, 31);
            this.numericUpDownPriceUnit.Name = "numericUpDownPriceUnit";
            this.numericUpDownPriceUnit.Size = new System.Drawing.Size(87, 20);
            this.numericUpDownPriceUnit.TabIndex = 9;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(248, 64);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(57, 13);
            this.label9.TabIndex = 8;
            this.label9.Text = "Unit Stock";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(248, 33);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(55, 13);
            this.label8.TabIndex = 7;
            this.label8.Text = "Price/Unit";
            // 
            // radioButtonMeasurable
            // 
            this.radioButtonMeasurable.AutoSize = true;
            this.radioButtonMeasurable.Location = new System.Drawing.Point(179, 109);
            this.radioButtonMeasurable.Name = "radioButtonMeasurable";
            this.radioButtonMeasurable.Size = new System.Drawing.Size(114, 17);
            this.radioButtonMeasurable.TabIndex = 6;
            this.radioButtonMeasurable.TabStop = true;
            this.radioButtonMeasurable.Text = "Measurable (kg/ltr)";
            this.radioButtonMeasurable.UseVisualStyleBackColor = true;
            // 
            // radioButtonCountable
            // 
            this.radioButtonCountable.AutoSize = true;
            this.radioButtonCountable.Location = new System.Drawing.Point(74, 109);
            this.radioButtonCountable.Name = "radioButtonCountable";
            this.radioButtonCountable.Size = new System.Drawing.Size(99, 17);
            this.radioButtonCountable.TabIndex = 5;
            this.radioButtonCountable.TabStop = true;
            this.radioButtonCountable.Text = "Countable (pcs)";
            this.radioButtonCountable.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(15, 111);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 13);
            this.label7.TabIndex = 4;
            this.label7.Text = "Unit Type";
            // 
            // comboBoxCategoryProducts
            // 
            this.comboBoxCategoryProducts.FormattingEnabled = true;
            this.comboBoxCategoryProducts.Location = new System.Drawing.Point(70, 66);
            this.comboBoxCategoryProducts.Name = "comboBoxCategoryProducts";
            this.comboBoxCategoryProducts.Size = new System.Drawing.Size(121, 21);
            this.comboBoxCategoryProducts.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 69);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(49, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "Category";
            // 
            // textBoxProductName
            // 
            this.textBoxProductName.Location = new System.Drawing.Point(70, 30);
            this.textBoxProductName.Name = "textBoxProductName";
            this.textBoxProductName.Size = new System.Drawing.Size(121, 20);
            this.textBoxProductName.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 33);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Name";
            // 
            // groupBoxTransaction
            // 
            this.groupBoxTransaction.Controls.Add(this.buttonClear);
            this.groupBoxTransaction.Controls.Add(this.buttonBuy);
            this.groupBoxTransaction.Controls.Add(this.labelDeliveryCost);
            this.groupBoxTransaction.Controls.Add(this.labelTotal);
            this.groupBoxTransaction.Controls.Add(this.numericUpDownQuantity);
            this.groupBoxTransaction.Controls.Add(this.label4);
            this.groupBoxTransaction.Controls.Add(this.label3);
            this.groupBoxTransaction.Controls.Add(this.label2);
            this.groupBoxTransaction.Location = new System.Drawing.Point(470, 385);
            this.groupBoxTransaction.Name = "groupBoxTransaction";
            this.groupBoxTransaction.Size = new System.Drawing.Size(318, 148);
            this.groupBoxTransaction.TabIndex = 4;
            this.groupBoxTransaction.TabStop = false;
            this.groupBoxTransaction.Text = "Transactional Area";
            // 
            // buttonClear
            // 
            this.buttonClear.Location = new System.Drawing.Point(207, 59);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(75, 23);
            this.buttonClear.TabIndex = 7;
            this.buttonClear.Text = "Clear";
            this.buttonClear.UseVisualStyleBackColor = true;
            // 
            // buttonBuy
            // 
            this.buttonBuy.Location = new System.Drawing.Point(207, 23);
            this.buttonBuy.Name = "buttonBuy";
            this.buttonBuy.Size = new System.Drawing.Size(75, 23);
            this.buttonBuy.TabIndex = 6;
            this.buttonBuy.Text = "Buy Item";
            this.buttonBuy.UseVisualStyleBackColor = true;
            this.buttonBuy.Click += new System.EventHandler(this.buttonBuy_Click);
            // 
            // labelDeliveryCost
            // 
            this.labelDeliveryCost.AutoSize = true;
            this.labelDeliveryCost.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDeliveryCost.Location = new System.Drawing.Point(97, 87);
            this.labelDeliveryCost.Name = "labelDeliveryCost";
            this.labelDeliveryCost.Size = new System.Drawing.Size(14, 13);
            this.labelDeliveryCost.TabIndex = 5;
            this.labelDeliveryCost.Text = "0";
            // 
            // labelTotal
            // 
            this.labelTotal.AutoSize = true;
            this.labelTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTotal.Location = new System.Drawing.Point(97, 59);
            this.labelTotal.Name = "labelTotal";
            this.labelTotal.Size = new System.Drawing.Size(14, 13);
            this.labelTotal.TabIndex = 4;
            this.labelTotal.Text = "0";
            // 
            // numericUpDownQuantity
            // 
            this.numericUpDownQuantity.Location = new System.Drawing.Point(97, 26);
            this.numericUpDownQuantity.Name = "numericUpDownQuantity";
            this.numericUpDownQuantity.Size = new System.Drawing.Size(88, 20);
            this.numericUpDownQuantity.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 87);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Delivery Cost";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 59);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Total";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Quantity";
            // 
            // CustomerProducts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 610);
            this.Controls.Add(this.groupBoxTransaction);
            this.Controls.Add(this.groupBoxDetails);
            this.Controls.Add(this.labelError);
            this.Controls.Add(this.groupBoxList);
            this.Controls.Add(this.label1);
            this.Name = "CustomerProducts";
            this.Text = "CustomerProducts";
            this.Load += new System.EventHandler(this.CustomerProducts_Load);
            this.groupBoxList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewProducts)).EndInit();
            this.groupBoxDetails.ResumeLayout(false);
            this.groupBoxDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDetailsUnitStock)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPriceUnit)).EndInit();
            this.groupBoxTransaction.ResumeLayout(false);
            this.groupBoxTransaction.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownQuantity)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBoxList;
        private System.Windows.Forms.Label labelError;
        private System.Windows.Forms.GroupBox groupBoxDetails;
        private System.Windows.Forms.GroupBox groupBoxTransaction;
        private System.Windows.Forms.NumericUpDown numericUpDownQuantity;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonBuy;
        private System.Windows.Forms.Label labelDeliveryCost;
        private System.Windows.Forms.Label labelTotal;
        private System.Windows.Forms.ComboBox comboBoxCategoryProducts;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxProductName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.RadioButton radioButtonMeasurable;
        private System.Windows.Forms.RadioButton radioButtonCountable;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DataGridView dataGridViewProducts;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown numericUpDownDetailsUnitStock;
        private System.Windows.Forms.NumericUpDown numericUpDownPriceUnit;
        private System.Windows.Forms.Label label9;
    }
}