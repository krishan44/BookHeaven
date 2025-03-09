namespace BookHeaven
{
    partial class History
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(History));
            this.dataGridViewOrders = new System.Windows.Forms.DataGridView();
            this.txtOrderID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtBookName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnBack = new System.Windows.Forms.Button();
            this.cmbCusID = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOrders)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewOrders
            // 
            this.dataGridViewOrders.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridViewOrders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewOrders.Location = new System.Drawing.Point(12, 126);
            this.dataGridViewOrders.Name = "dataGridViewOrders";
            this.dataGridViewOrders.Size = new System.Drawing.Size(581, 312);
            this.dataGridViewOrders.TabIndex = 0;
            // 
            // txtOrderID
            // 
            this.txtOrderID.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOrderID.Location = new System.Drawing.Point(16, 88);
            this.txtOrderID.Name = "txtOrderID";
            this.txtOrderID.Size = new System.Drawing.Size(107, 22);
            this.txtOrderID.TabIndex = 38;
            this.txtOrderID.TextChanged += new System.EventHandler(this.txtOrderID_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 21);
            this.label1.TabIndex = 39;
            this.label1.Text = "Order ID";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(152, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(99, 21);
            this.label3.TabIndex = 41;
            this.label3.Text = "Book Name";
            // 
            // txtBookName
            // 
            this.txtBookName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBookName.Location = new System.Drawing.Point(156, 88);
            this.txtBookName.Name = "txtBookName";
            this.txtBookName.Size = new System.Drawing.Size(107, 22);
            this.txtBookName.TabIndex = 40;
            this.txtBookName.TextChanged += new System.EventHandler(this.txtBookName_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(304, 64);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(107, 21);
            this.label4.TabIndex = 43;
            this.label4.Text = "Customer ID";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(258, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 26);
            this.label2.TabIndex = 46;
            this.label2.Text = "History";
            // 
            // btnBack
            // 
            this.btnBack.BackColor = System.Drawing.Color.Transparent;
            this.btnBack.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBack.BackgroundImage")));
            this.btnBack.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBack.FlatAppearance.BorderSize = 0;
            this.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBack.ForeColor = System.Drawing.Color.Transparent;
            this.btnBack.Location = new System.Drawing.Point(12, 9);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(35, 32);
            this.btnBack.TabIndex = 50;
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // cmbCusID
            // 
            this.cmbCusID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCusID.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCusID.FormattingEnabled = true;
            this.cmbCusID.Location = new System.Drawing.Point(308, 86);
            this.cmbCusID.Name = "cmbCusID";
            this.cmbCusID.Size = new System.Drawing.Size(116, 24);
            this.cmbCusID.TabIndex = 51;
            this.cmbCusID.TextChanged += new System.EventHandler(this.cmbCusID_SelectedIndexChanged);
            // 
            // History
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(605, 450);
            this.ControlBox = false;
            this.Controls.Add(this.cmbCusID);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtBookName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtOrderID);
            this.Controls.Add(this.dataGridViewOrders);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "History";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Book Heaven || Book System";
            this.Load += new System.EventHandler(this.History_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOrders)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewOrders;
        private System.Windows.Forms.TextBox txtOrderID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtBookName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.ComboBox cmbCusID;
    }
}