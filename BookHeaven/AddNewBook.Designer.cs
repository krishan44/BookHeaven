namespace BookHeaven
{
    partial class AddNewBook
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddNewBook));
            this.label1 = new System.Windows.Forms.Label();
            this.txtBookTitle = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtAuthor = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.picCover = new System.Windows.Forms.PictureBox();
            this.btnUploadCover = new System.Windows.Forms.Button();
            this.txtISBN = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtSupplier = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtPrice = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cmbGenre = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtStock = new System.Windows.Forms.TextBox();
            this.cmbSupId = new System.Windows.Forms.ComboBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picCover)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(52, 102);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 21);
            this.label1.TabIndex = 41;
            this.label1.Text = "Book Title";
            // 
            // txtBookTitle
            // 
            this.txtBookTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBookTitle.Location = new System.Drawing.Point(157, 102);
            this.txtBookTitle.Multiline = true;
            this.txtBookTitle.Name = "txtBookTitle";
            this.txtBookTitle.Size = new System.Drawing.Size(200, 38);
            this.txtBookTitle.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(235, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(165, 26);
            this.label2.TabIndex = 47;
            this.label2.Text = "Add New Book";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(52, 166);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 21);
            this.label3.TabIndex = 48;
            this.label3.Text = "Author";
            // 
            // txtAuthor
            // 
            this.txtAuthor.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAuthor.Location = new System.Drawing.Point(157, 166);
            this.txtAuthor.Name = "txtAuthor";
            this.txtAuthor.Size = new System.Drawing.Size(200, 22);
            this.txtAuthor.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(55, 260);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 21);
            this.label4.TabIndex = 50;
            this.label4.Text = "ISBN";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(456, 92);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(106, 21);
            this.label5.TabIndex = 51;
            this.label5.Text = "Cover Photo";
            // 
            // picCover
            // 
            this.picCover.Location = new System.Drawing.Point(435, 116);
            this.picCover.Name = "picCover";
            this.picCover.Size = new System.Drawing.Size(145, 151);
            this.picCover.TabIndex = 52;
            this.picCover.TabStop = false;
            // 
            // btnUploadCover
            // 
            this.btnUploadCover.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnUploadCover.Location = new System.Drawing.Point(460, 131);
            this.btnUploadCover.Name = "btnUploadCover";
            this.btnUploadCover.Size = new System.Drawing.Size(102, 33);
            this.btnUploadCover.TabIndex = 9;
            this.btnUploadCover.Text = "Upload";
            this.btnUploadCover.UseVisualStyleBackColor = false;
            this.btnUploadCover.Click += new System.EventHandler(this.btnUploadCover_Click);
            // 
            // txtISBN
            // 
            this.txtISBN.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtISBN.Location = new System.Drawing.Point(157, 259);
            this.txtISBN.Name = "txtISBN";
            this.txtISBN.Size = new System.Drawing.Size(200, 22);
            this.txtISBN.TabIndex = 4;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(52, 355);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(96, 21);
            this.label6.TabIndex = 55;
            this.label6.Text = "Supplier ID";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(307, 354);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(72, 21);
            this.label7.TabIndex = 56;
            this.label7.Text = "Supplier";
            // 
            // txtSupplier
            // 
            this.txtSupplier.Enabled = false;
            this.txtSupplier.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSupplier.Location = new System.Drawing.Point(394, 354);
            this.txtSupplier.Multiline = true;
            this.txtSupplier.Name = "txtSupplier";
            this.txtSupplier.Size = new System.Drawing.Size(186, 37);
            this.txtSupplier.TabIndex = 8;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(100, 307);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(48, 21);
            this.label8.TabIndex = 59;
            this.label8.Text = "Price";
            // 
            // txtPrice
            // 
            this.txtPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPrice.Location = new System.Drawing.Point(157, 306);
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.Size = new System.Drawing.Size(61, 22);
            this.txtPrice.TabIndex = 5;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(52, 210);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(55, 21);
            this.label9.TabIndex = 61;
            this.label9.Text = "Genre";
            // 
            // cmbGenre
            // 
            this.cmbGenre.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGenre.FormattingEnabled = true;
            this.cmbGenre.Items.AddRange(new object[] {
            "--Select--",
            "Fantasy",
            "Science Fiction (Sci-Fi)",
            "Mystery",
            "Thriller",
            "Horror",
            "Romance",
            "Historical",
            "Fiction",
            "Adventure",
            "Dystopian",
            "Magical ",
            "Realism"});
            this.cmbGenre.Location = new System.Drawing.Point(157, 210);
            this.cmbGenre.Name = "cmbGenre";
            this.cmbGenre.Size = new System.Drawing.Size(200, 21);
            this.cmbGenre.TabIndex = 3;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(236, 306);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(54, 21);
            this.label10.TabIndex = 63;
            this.label10.Text = "Stock";
            // 
            // txtStock
            // 
            this.txtStock.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtStock.Location = new System.Drawing.Point(296, 305);
            this.txtStock.Name = "txtStock";
            this.txtStock.Size = new System.Drawing.Size(61, 22);
            this.txtStock.TabIndex = 6;
            // 
            // cmbSupId
            // 
            this.cmbSupId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSupId.Location = new System.Drawing.Point(157, 357);
            this.cmbSupId.Name = "cmbSupId";
            this.cmbSupId.Size = new System.Drawing.Size(133, 21);
            this.cmbSupId.TabIndex = 7;
            this.cmbSupId.SelectedIndexChanged += new System.EventHandler(this.cmbSupId_SelectedIndexChanged_1);
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnAdd.Location = new System.Drawing.Point(265, 426);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(102, 33);
            this.btnAdd.TabIndex = 10;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
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
            this.btnBack.TabIndex = 67;
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // AddNewBook
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(619, 471);
            this.ControlBox = false;
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.cmbSupId);
            this.Controls.Add(this.txtStock);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.cmbGenre);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtPrice);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtSupplier);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtISBN);
            this.Controls.Add(this.btnUploadCover);
            this.Controls.Add(this.picCover);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtAuthor);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtBookTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AddNewBook";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Book Heaven || Book System";
            this.Load += new System.EventHandler(this.AddNewBook_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picCover)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBookTitle;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtAuthor;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.PictureBox picCover;
        private System.Windows.Forms.Button btnUploadCover;
        private System.Windows.Forms.TextBox txtISBN;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtSupplier;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtPrice;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cmbGenre;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtStock;
        private System.Windows.Forms.ComboBox cmbSupId;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnBack;
    }
}