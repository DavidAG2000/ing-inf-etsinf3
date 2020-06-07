namespace EcoScooter.GUI
{
    partial class Cas8_GetRoutes
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
            this.driverLicensedateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.addPersonButton = new System.Windows.Forms.Button();
            this.reservationsdataGridView = new System.Windows.Forms.DataGridView();
            this.Customer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PickUpOffice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ReturnOffice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Category = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NumDrivers = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.reservationsdataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // driverLicensedateTimePicker
            // 
            this.driverLicensedateTimePicker.Location = new System.Drawing.Point(211, 24);
            this.driverLicensedateTimePicker.Margin = new System.Windows.Forms.Padding(4);
            this.driverLicensedateTimePicker.Name = "driverLicensedateTimePicker";
            this.driverLicensedateTimePicker.Size = new System.Drawing.Size(265, 22);
            this.driverLicensedateTimePicker.TabIndex = 1;
            this.driverLicensedateTimePicker.Value = new System.DateTime(2019, 12, 10, 0, 0, 0, 0);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(107, 24);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(99, 20);
            this.label6.TabIndex = 31;
            this.label6.Text = "Inici periode";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(211, 62);
            this.dateTimePicker1.Margin = new System.Windows.Forms.Padding(4);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(265, 22);
            this.dateTimePicker1.TabIndex = 2;
            this.dateTimePicker1.Value = new System.DateTime(2019, 12, 12, 0, 0, 0, 0);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(120, 62);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 20);
            this.label1.TabIndex = 33;
            this.label1.Text = "Fi periode";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(530, 60);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(139, 28);
            this.button1.TabIndex = 5;
            this.button1.Text = "Cancelar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // addPersonButton
            // 
            this.addPersonButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addPersonButton.Location = new System.Drawing.Point(530, 24);
            this.addPersonButton.Margin = new System.Windows.Forms.Padding(4);
            this.addPersonButton.Name = "addPersonButton";
            this.addPersonButton.Size = new System.Drawing.Size(139, 28);
            this.addPersonButton.TabIndex = 3;
            this.addPersonButton.Text = "Mostrar alquilers";
            this.addPersonButton.UseVisualStyleBackColor = true;
            this.addPersonButton.Click += new System.EventHandler(this.addPersonButton_Click);
            // 
            // reservationsdataGridView
            // 
            this.reservationsdataGridView.AllowUserToAddRows = false;
            this.reservationsdataGridView.AllowUserToDeleteRows = false;
            this.reservationsdataGridView.AllowUserToResizeColumns = false;
            this.reservationsdataGridView.AllowUserToResizeRows = false;
            this.reservationsdataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.reservationsdataGridView.BackgroundColor = System.Drawing.SystemColors.Window;
            this.reservationsdataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.reservationsdataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Customer,
            this.PickUpOffice,
            this.ReturnOffice,
            this.Category,
            this.NumDrivers});
            this.reservationsdataGridView.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.reservationsdataGridView.Location = new System.Drawing.Point(0, 118);
            this.reservationsdataGridView.Margin = new System.Windows.Forms.Padding(4);
            this.reservationsdataGridView.Name = "reservationsdataGridView";
            this.reservationsdataGridView.ReadOnly = true;
            this.reservationsdataGridView.RowHeadersVisible = false;
            this.reservationsdataGridView.RowHeadersWidth = 62;
            this.reservationsdataGridView.Size = new System.Drawing.Size(788, 242);
            this.reservationsdataGridView.TabIndex = 4;
            this.reservationsdataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.reservationsdataGridView_CellContentClick);
            // 
            // Customer
            // 
            this.Customer.DataPropertyName = "ds_Customer";
            this.Customer.FillWeight = 101.7259F;
            this.Customer.HeaderText = "Estació origen";
            this.Customer.MinimumWidth = 8;
            this.Customer.Name = "Customer";
            this.Customer.ReadOnly = true;
            // 
            // PickUpOffice
            // 
            this.PickUpOffice.DataPropertyName = "ds_PickUpOffice";
            this.PickUpOffice.FillWeight = 101.7259F;
            this.PickUpOffice.HeaderText = "Estació final";
            this.PickUpOffice.MinimumWidth = 8;
            this.PickUpOffice.Name = "PickUpOffice";
            this.PickUpOffice.ReadOnly = true;
            // 
            // ReturnOffice
            // 
            this.ReturnOffice.DataPropertyName = "ds_ReturnOffice";
            this.ReturnOffice.FillWeight = 101.7259F;
            this.ReturnOffice.HeaderText = "Data de inici";
            this.ReturnOffice.MinimumWidth = 8;
            this.ReturnOffice.Name = "ReturnOffice";
            this.ReturnOffice.ReadOnly = true;
            // 
            // Category
            // 
            this.Category.DataPropertyName = "ds_Category";
            this.Category.FillWeight = 101.7259F;
            this.Category.HeaderText = "Data de tornada";
            this.Category.MinimumWidth = 8;
            this.Category.Name = "Category";
            this.Category.ReadOnly = true;
            // 
            // NumDrivers
            // 
            this.NumDrivers.DataPropertyName = "ds_NumDrivers";
            this.NumDrivers.FillWeight = 91.37056F;
            this.NumDrivers.HeaderText = "Cost";
            this.NumDrivers.MinimumWidth = 8;
            this.NumDrivers.Name = "NumDrivers";
            this.NumDrivers.ReadOnly = true;
            // 
            // Cas8_GetRoutes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(788, 360);
            this.Controls.Add(this.reservationsdataGridView);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.addPersonButton);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.driverLicensedateTimePicker);
            this.Controls.Add(this.label6);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.Name = "Cas8_GetRoutes";
            this.Text = "Cas8_GetRoutes";
            ((System.ComponentModel.ISupportInitialize)(this.reservationsdataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected System.Windows.Forms.DateTimePicker driverLicensedateTimePicker;
        private System.Windows.Forms.Label label6;
        protected System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label1;
        protected System.Windows.Forms.Button button1;
        protected System.Windows.Forms.Button addPersonButton;
        private System.Windows.Forms.DataGridView reservationsdataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn Customer;
        private System.Windows.Forms.DataGridViewTextBoxColumn PickUpOffice;
        private System.Windows.Forms.DataGridViewTextBoxColumn ReturnOffice;
        private System.Windows.Forms.DataGridViewTextBoxColumn Category;
        private System.Windows.Forms.DataGridViewTextBoxColumn NumDrivers;
    }
}