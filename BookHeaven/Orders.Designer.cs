namespace BookHeaven
{
    partial class Orders
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Orders));
            this.label10 = new System.Windows.Forms.Label();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.dataGridViewOrders = new System.Windows.Forms.DataGridView();
            this.txtOrderID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnBack = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOrders)).BeginInit();
            this.SuspendLayout();
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(370, 339);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(56, 21);
            this.label10.TabIndex = 26;
            this.label10.Text = "Status";
            // 
            // cmbStatus
            // 
            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Items.AddRange(new object[] {
            "--Select--",
            "Delivered",
            "Picked "});
            this.cmbStatus.Location = new System.Drawing.Point(432, 336);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(156, 24);
            this.cmbStatus.TabIndex = 27;
            // 
            // dataGridViewOrders
            // 
            this.dataGridViewOrders.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridViewOrders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewOrders.Location = new System.Drawing.Point(72, 53);
            this.dataGridViewOrders.Name = "dataGridViewOrders";
            this.dataGridViewOrders.Size = new System.Drawing.Size(638, 245);
            this.dataGridViewOrders.TabIndex = 31;
            this.dataGridViewOrders.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewOrders_CellContentClick);
            // 
            // txtOrderID
            // 
            this.txtOrderID.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOrderID.Location = new System.Drawing.Point(220, 338);
            this.txtOrderID.Name = "txtOrderID";
            this.txtOrderID.Size = new System.Drawing.Size(127, 22);
            this.txtOrderID.TabIndex = 32;
            this.txtOrderID.TextChanged += new System.EventHandler(this.txtOrderID_TextChanged_1);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(136, 339);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 21);
            this.label1.TabIndex = 33;
            this.label1.Text = "Order ID";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(310, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(166, 26);
            this.label2.TabIndex = 37;
            this.label2.Text = "Update Orders";
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
            this.btnBack.TabIndex = 48;
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.LightGreen;
            this.btnDelete.Location = new System.Drawing.Point(240, 389);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(132, 39);
            this.btnDelete.TabIndex = 50;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.BackColor = System.Drawing.Color.LightGreen;
            this.btnUpdate.Location = new System.Drawing.Point(378, 389);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(132, 39);
            this.btnUpdate.TabIndex = 51;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = false;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // Orders
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(747, 450);
            this.ControlBox = false;
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtOrderID);
            this.Controls.Add(this.dataGridViewOrders);
            this.Controls.Add(this.cmbStatus);
            this.Controls.Add(this.label10);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Orders";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Book Heaven || Book System";
            this.Load += new System.EventHandler(this.Orders_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOrders)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.DataGridView dataGridViewOrders;
        private System.Windows.Forms.TextBox txtOrderID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnUpdate;
    }
}