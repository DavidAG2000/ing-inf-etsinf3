using EcoScooter.Entities;
using EcoScooter.Services;
using System;
using System.Windows.Forms;

namespace EcoScooter.GUI
{
    public partial class Cas4_RegistrarScooter : Form
    {
        private readonly IEcoScooterService service;
        public Cas4_RegistrarScooter(IEcoScooterService service)
        {
            InitializeComponent();
            this.service = service;
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void driverLicensedateTimePicker_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void addPersonButton_Click(object sender, EventArgs e)
        {
            try
            {
                String st = null;
                ScooterState state;
                if (radioButton1.Checked)
                {
                    state = ScooterState.available;
                    st = citytextBox.Text.Trim();
                }
                else
                {
                    state = ScooterState.inMaintenance;
                }
                service.RegisterScooter(driverLicensedateTimePicker.Value, state, st);
                MessageBox.Show("Scooter afegit correctament");
                this.Close();
            }
            catch (ServiceException exc)
            {
                MessageBox.Show(exc.Message, "Error");
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                citytextBox.Enabled = true;
            }
            else
            {
                citytextBox.Enabled = false;
            }
        }
    }
}
