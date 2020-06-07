using EcoScooter.Services;
using System;
using System.Windows.Forms;

namespace EcoScooter.GUI
{
    public partial class Cas6_ReturnScooter : Form
    {
        private readonly IEcoScooterService service;
        public Cas6_ReturnScooter(IEcoScooterService service)
        {
            InitializeComponent();
            this.service = service;
        }

        private void addPersonButton_Click(object sender, EventArgs e)
        {
            try
            {
                service.ReturnScooter(dnitextBox.Text);
                DialogResult result = MessageBox.Show("Ha ocorregut cap incidencia al recorregut?", "Incident", MessageBoxButtons.YesNoCancel);
                if (result == DialogResult.Yes)
                {
                    new Cas7_RegisterIncident(service).ShowDialog();
                }
                MessageBox.Show("Patinet retornat correctament");
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

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
