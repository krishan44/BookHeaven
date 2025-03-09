namespace BookHeaven
{
    partial class OrderPayment
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OrderPayment));
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.DeliveryGrp = new System.Windows.Forms.GroupBox();
            this.Delivery = new System.Windows.Forms.RadioButton();
            this.PickUp = new System.Windows.Forms.RadioButton();
            this.txtCustomer = new System.Windows.Forms.TextBox();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.txtContact = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtTotal = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.cmbCusID = new System.Windows.Forms.ComboBox();
            this.btnBack = new System.Windows.Forms.Button();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.DeliveryGrp.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(274, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(218, 29);
            this.label2.TabIndex = 5;
            this.label2.Text = "Order Confirmation";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(15, 106);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 21);
            this.label3.TabIndex = 6;
            this.label3.Text = "Customer";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(15, 145);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 21);
            this.label4.TabIndex = 7;
            this.label4.Text = "Address";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(19, 206);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 21);
            this.label5.TabIndex = 8;
            this.label5.Text = "Contact";
            // 
            // DeliveryGrp
            // 
            this.DeliveryGrp.BackColor = System.Drawing.Color.Transparent;
            this.DeliveryGrp.Controls.Add(this.Delivery);
            this.DeliveryGrp.Controls.Add(this.PickUp);
            this.DeliveryGrp.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DeliveryGrp.Location = new System.Drawing.Point(396, 65);
            this.DeliveryGrp.Name = "DeliveryGrp";
            this.DeliveryGrp.Size = new System.Drawing.Size(334, 96);
            this.DeliveryGrp.TabIndex = 9;
            this.DeliveryGrp.TabStop = false;
            this.DeliveryGrp.Text = "Delivery Method";
            // 
            // Delivery
            // 
            this.Delivery.AutoSize = true;
            this.Delivery.Location = new System.Drawing.Point(30, 56);
            this.Delivery.Name = "Delivery";
            this.Delivery.Size = new System.Drawing.Size(75, 20);
            this.Delivery.TabIndex = 1;
            this.Delivery.TabStop = true;
            this.Delivery.Text = "Delivery";
            this.Delivery.UseVisualStyleBackColor = true;
            // 
            // PickUp
            // 
            this.PickUp.AutoSize = true;
            this.PickUp.Location = new System.Drawing.Point(30, 21);
            this.PickUp.Name = "PickUp";
            this.PickUp.Size = new System.Drawing.Size(72, 20);
            this.PickUp.TabIndex = 0;
            this.PickUp.TabStop = true;
            this.PickUp.Text = "Pick Up";
            this.PickUp.UseVisualStyleBackColor = true;
            // 
            // txtCustomer
            // 
            this.txtCustomer.Enabled = false;
            this.txtCustomer.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCustomer.Location = new System.Drawing.Point(133, 106);
            this.txtCustomer.Name = "txtCustomer";
            this.txtCustomer.Size = new System.Drawing.Size(233, 22);
            this.txtCustomer.TabIndex = 11;
            // 
            // txtAddress
            // 
            this.txtAddress.Enabled = false;
            this.txtAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAddress.Location = new System.Drawing.Point(133, 145);
            this.txtAddress.Multiline = true;
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(233, 45);
            this.txtAddress.TabIndex = 12;
            // 
            // txtContact
            // 
            this.txtContact.Enabled = false;
            this.txtContact.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtContact.Location = new System.Drawing.Point(133, 205);
            this.txtContact.Name = "txtContact";
            this.txtContact.Size = new System.Drawing.Size(233, 22);
            this.txtContact.TabIndex = 13;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(16, 67);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(107, 21);
            this.label6.TabIndex = 15;
            this.label6.Text = "Customer ID";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(238, 268);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(48, 21);
            this.label9.TabIndex = 19;
            this.label9.Text = "Total";
            // 
            // txtTotal
            // 
            this.txtTotal.Enabled = false;
            this.txtTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotal.Location = new System.Drawing.Point(310, 267);
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.Size = new System.Drawing.Size(127, 22);
            this.txtTotal.TabIndex = 22;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.IndianRed;
            this.button2.Location = new System.Drawing.Point(222, 307);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(132, 39);
            this.button2.TabIndex = 25;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // cmbCusID
            // 
            this.cmbCusID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCusID.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCusID.FormattingEnabled = true;
            this.cmbCusID.Location = new System.Drawing.Point(133, 69);
            this.cmbCusID.Name = "cmbCusID";
            this.cmbCusID.Size = new System.Drawing.Size(233, 24);
            this.cmbCusID.TabIndex = 26;
            this.cmbCusID.SelectedIndexChanged += new System.EventHandler(this.cmbCusID_SelectedIndexChanged);
            // 
            // btnBack
            // 
            this.btnBack.BackColor = System.Drawing.Color.Transparent;
            this.btnBack.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBack.BackgroundImage")));
            this.btnBack.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBack.FlatAppearance.BorderSize = 0;
            this.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBack.ForeColor = System.Drawing.Color.Transparent;
            this.btnBack.Location = new System.Drawing.Point(12, 12);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(35, 32);
            this.btnBack.TabIndex = 49;
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // txtEmail
            // 
            this.txtEmail.Enabled = false;
            this.txtEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmail.Location = new System.Drawing.Point(450, 207);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(233, 22);
            this.txtEmail.TabIndex = 50;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(383, 207);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(51, 21);
            this.label11.TabIndex = 51;
            this.label11.Text = "Email";
            // 
            // btnConfirm
            // 
            this.btnConfirm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnConfirm.Location = new System.Drawing.Point(360, 307);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(132, 39);
            this.btnConfirm.TabIndex = 53;
            this.btnConfirm.Text = "Confirm";
            this.btnConfirm.UseVisualStyleBackColor = false;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // OrderPayment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(742, 369);
            this.ControlBox = false;
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.cmbCusID);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.txtTotal);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtContact);
            this.Controls.Add(this.txtAddress);
            this.Controls.Add(this.txtCustomer);
            this.Controls.Add(this.DeliveryGrp);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "OrderPayment";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Book Heaven || Book System";
            this.Load += new System.EventHandler(this.OrderPayment_Load);
            this.DeliveryGrp.ResumeLayout(false);
            this.DeliveryGrp.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox DeliveryGrp;
        private System.Windows.Forms.RadioButton Delivery;
        private System.Windows.Forms.RadioButton PickUp;
        private System.Windows.Forms.TextBox txtCustomer;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.TextBox txtContact;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtTotal;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ComboBox cmbCusID;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btnConfirm;
    }
}