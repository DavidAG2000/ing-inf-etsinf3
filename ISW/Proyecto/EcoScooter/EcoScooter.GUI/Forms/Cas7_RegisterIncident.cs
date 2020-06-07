using EcoScooter.Services;
using System;
using System.Windows.Forms;

namespace EcoScooter.GUI
{
    public partial class Cas7_RegisterIncident : Form
    {
        private readonly IEcoScooterService service;
        public Cas7_RegisterIncident(IEcoScooterService service)
        {
            InitializeComponent();
            this.service = service;
        }

        private void label1_Click(object sender, EventArgs e)
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
                service.RegisterIncident(dnitextBox.Text.Trim(), driverLicensedateTimePicker.Value, service.LastRentalId());
                MessageBox.Show("Incident registrat correctament");
                this.Close();
            }
            catch (ServiceException exc)
            {
                MessageBox.Show(exc.Message, "Error");
            }
        }

        private void Cas7_RegisterIncident_Load(object sender, EventArgs e)
        {

        }
    }
}
