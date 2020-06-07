using EcoScooter.Services;
using System;
using System.Windows.Forms;

namespace EcoScooter.GUI
{
    public partial class Cas3_RegisterStation : Form
    {
        private readonly IEcoScooterService service;
        public Cas3_RegisterStation(IEcoScooterService service)
        {
            this.service = service;
            InitializeComponent();
        }

        private void addPersonButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (nametextBox.Text == null || nametextBox.Text.Trim().Equals(""))
                {
                    throw new ServiceException("ERROR: El camp latitud està buit");
                }
                if (addresstextBox.Text == null || addresstextBox.Text.Trim().Equals(""))
                {
                    throw new ServiceException("ERROR:El camp longitud està buit");
                }
                service.RegisterStation(dnitextBox.Text.Trim(), Double.Parse(nametextBox.Text.Trim()), Double.Parse(addresstextBox.Text.Trim()),
                    citytextBox.Text.Trim());
                MessageBox.Show("Estació creada correctament");
                this.Close();
            }
            catch (ServiceException exc)
            {
                MessageBox.Show(exc.Message, "Error");
            }
        }

        private void dnitextBox_TextChanged(object sender, EventArgs e)
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

        private void nametextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void nametextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void addresstextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }
    }
}
