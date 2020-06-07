namespace EcoScooter.GUI
{
    partial class Cas3_RegisterStation
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
            this.citytextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.addresstextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.nametextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dnitextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.addPersonButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // citytextBox
            // 
            this.citytextBox.Location = new System.Drawing.Point(165, 190);
            this.citytextBox.Margin = new System.Windows.Forms.Padding(4);
            this.citytextBox.Name = "citytextBox";
            this.citytextBox.Size = new System.Drawing.Size(132, 22);
            this.citytextBox.TabIndex = 4;
            this.citytextBox.TextChanged += new System.EventHandler(this.citytextBox_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(59, 193);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 20);
            this.label4.TabIndex = 14;
            this.label4.Text = "Identificador";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // addresstextBox
            // 
            this.addresstextBox.Location = new System.Drawing.Point(165, 137);
            this.addresstextBox.Margin = new System.Windows.Forms.Padding(4);
            this.addresstextBox.Name = "addresstextBox";
            this.addresstextBox.Size = new System.Drawing.Size(212, 22);
            this.addresstextBox.TabIndex = 3;
            this.addresstextBox.TextChanged += new System.EventHandler(this.addresstextBox_TextChanged);
            this.addresstextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.addresstextBox_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(81, 140);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 20);
            this.label3.TabIndex = 12;
            this.label3.Text = "Longitud";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // nametextBox
            // 
            this.nametextBox.Location = new System.Drawing.Point(165, 86);
            this.nametextBox.Margin = new System.Windows.Forms.Padding(4);
            this.nametextBox.Name = "nametextBox";
            this.nametextBox.Size = new System.Drawing.Size(212, 22);
            this.nametextBox.TabIndex = 2;
            this.nametextBox.TextChanged += new System.EventHandler(this.nametextBox_TextChanged);
            this.nametextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.nametextBox_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(93, 90);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 20);
            this.label2.TabIndex = 10;
            this.label2.Text = "Latitud";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // dnitextBox
            // 
            this.dnitextBox.Location = new System.Drawing.Point(165, 37);
            this.dnitextBox.Margin = new System.Windows.Forms.Padding(4);
            this.dnitextBox.Name = "dnitextBox";
            this.dnitextBox.Size = new System.Drawing.Size(132, 22);
            this.dnitextBox.TabIndex = 1;
            this.dnitextBox.TextChanged += new System.EventHandler(this.dnitextBox_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(85, 39);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 20);
            this.label1.TabIndex = 8;
            this.label1.Text = "Direcció";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // addPersonButton
            // 
            this.addPersonButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addPersonButton.Location = new System.Drawing.Point(84, 256);
            this.addPersonButton.Margin = new System.Windows.Forms.Padding(4);
            this.addPersonButton.Name = "addPersonButton";
            this.addPersonButton.Size = new System.Drawing.Size(139, 28);
            this.addPersonButton.TabIndex = 5;
            this.addPersonButton.Text = "Registrar estació";
            this.addPersonButton.UseVisualStyleBackColor = true;
            this.addPersonButton.Click += new System.EventHandler(this.addPersonButton_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(229, 256);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(139, 28);
            this.button1.TabIndex = 6;
            this.button1.Text = "Cancelar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(27, 19);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(130, 20);
            this.label8.TabIndex = 29;
            this.label8.Text = "Dades estació";
            this.label8.Click += new System.EventHandler(this.label8_Click);
            // 
            // Cas3_RegisterStation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(475, 321);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.addPersonButton);
            this.Controls.Add(this.citytextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.addresstextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.nametextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dnitextBox);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.Name = "Cas3_RegisterStation";
            this.Text = "Cas3_RegistrarEstacio";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected System.Windows.Forms.TextBox citytextBox;
        private System.Windows.Forms.Label label4;
        protected System.Windows.Forms.TextBox addresstextBox;
        private System.Windows.Forms.Label label3;
        protected System.Windows.Forms.TextBox nametextBox;
        private System.Windows.Forms.Label label2;
        protected System.Windows.Forms.TextBox dnitextBox;
        private System.Windows.Forms.Label label1;
        protected System.Windows.Forms.Button addPersonButton;
        protected System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label8;
    }
}