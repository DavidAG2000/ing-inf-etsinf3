using EcoScooter.Services;
using System;
using System.Windows.Forms;

namespace EcoScooter.GUI
{
    public partial class Cas5_RentScooter : Form
    {
        private readonly IEcoScooterService service;
        public Cas5_RentScooter(IEcoScooterService service)
        {
            InitializeComponent();
            this.service = service;
        }

        private void addPersonButton_Click(object sender, EventArgs e)
        {
            try
            {
                service.RentScooter(dnitextBox.Text.Trim());
                MessageBox.Show("Lloguer disponible. Agafa el patinet amb les llums enceses");
                this.Close();
            }
            catch (ServiceException exc)
            {
                MessageBox.Show(exc.Message, "Error");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dnitextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
