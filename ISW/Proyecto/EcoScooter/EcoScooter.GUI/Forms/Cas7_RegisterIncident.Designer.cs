namespace EcoScooter.GUI
{
    partial class Cas7_RegisterIncident
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
            this.dnitextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.addPersonButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // driverLicensedateTimePicker
            // 
            this.driverLicensedateTimePicker.Location = new System.Drawing.Point(216, 128);
            this.driverLicensedateTimePicker.Margin = new System.Windows.Forms.Padding(4);
            this.driverLicensedateTimePicker.Name = "driverLicensedateTimePicker";
            this.driverLicensedateTimePicker.Size = new System.Drawing.Size(265, 22);
            this.driverLicensedateTimePicker.TabIndex = 2;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(95, 130);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(113, 20);
            this.label6.TabIndex = 29;
            this.label6.Text = "Data accident";
            // 
            // dnitextBox
            // 
            this.dnitextBox.Location = new System.Drawing.Point(216, 49);
            this.dnitextBox.Margin = new System.Windows.Forms.Padding(4);
            this.dnitextBox.Multiline = true;
            this.dnitextBox.Name = "dnitextBox";
            this.dnitextBox.Size = new System.Drawing.Size(265, 72);
            this.dnitextBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(28, 50);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(180, 20);
            this.label1.TabIndex = 25;
            this.label1.Text = "Descripció del incident";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(296, 202);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(139, 28);
            this.button1.TabIndex = 4;
            this.button1.Text = "Cancelar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // addPersonButton
            // 
            this.addPersonButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addPersonButton.Location = new System.Drawing.Point(150, 202);
            this.addPersonButton.Margin = new System.Windows.Forms.Padding(4);
            this.addPersonButton.Name = "addPersonButton";
            this.addPersonButton.Size = new System.Drawing.Size(139, 28);
            this.addPersonButton.TabIndex = 3;
            this.addPersonButton.Text = "Registrar incident";
            this.addPersonButton.UseVisualStyleBackColor = true;
            this.addPersonButton.Click += new System.EventHandler(this.addPersonButton_Click);
            // 
            // Cas7_RegisterIncident
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(583, 290);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.addPersonButton);
            this.Controls.Add(this.driverLicensedateTimePicker);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.dnitextBox);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.Name = "Cas7_RegisterIncident";
            this.Text = "Cas7_RegisterIncident";
            this.Load += new System.EventHandler(this.Cas7_RegisterIncident_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected System.Windows.Forms.DateTimePicker driverLicensedateTimePicker;
        private System.Windows.Forms.Label label6;
        protected System.Windows.Forms.TextBox dnitextBox;
        private System.Windows.Forms.Label label1;
        protected System.Windows.Forms.Button button1;
        protected System.Windows.Forms.Button addPersonButton;
    }
}