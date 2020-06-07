using EcoScooter.Services;
using System;
using System.Windows.Forms;

namespace EcoScooter.GUI
{
    public partial class Cas1_Registrarse : Form
    {
        private readonly IEcoScooterService service;
        public Cas1_Registrarse(IEcoScooterService service)
        {
            InitializeComponent();
            this.service = service;
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Cas1_Registrarse_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void driverLicensedateTimePicker_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void postalcodetextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void citytextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void addresstextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void addPersonButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (phonetextBox.Text == null || phonetextBox.Text.Trim().Equals(""))
                {
                    throw new ServiceException("ERROR: El camp telèfon està buit");
                }
                if (phonetextBox.Text.Length >= 10)
                {
                    throw new ServiceException("ERROR: El valor en el camp telèfon és massa llarg");
                }
                if (textBox2.Text == null || textBox2.Text.Trim().Equals(""))
                {
                    throw new ServiceException("ERROR: El camp CVV està buit");
                }
                if (textBox1.Text == null || textBox1.Text.Trim().Equals(""))
                {
                    throw new ServiceException("ERROR: El camp número està buit");
                }
                if (textBox1.Text.Length >= 9)
                {
                    throw new ServiceException("ERROR: El valor en el camp número és massa llarg");
                }
                service.RegisterUser(driverLicensedateTimePicker.Value, dnitextBox.Text.Trim(), citytextBox.Text.Trim(), nametextBox.Text.Trim(),
                    Int32.Parse(phonetextBox.Text.Trim()), Int32.Parse(textBox2.Text.Trim()), dateTimePicker1.Value, textBox3.Text.Trim(),
                    Int32.Parse(textBox1.Text.Trim()), textBox4.Text.Trim());
                MessageBox.Show("Usuari creat correctament");
                this.Close();
            }
            catch (ServiceException exc)
            {
                MessageBox.Show(exc.Message, "Error");
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void dnitextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void nametextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void phonetextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void citytextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void dnitextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
