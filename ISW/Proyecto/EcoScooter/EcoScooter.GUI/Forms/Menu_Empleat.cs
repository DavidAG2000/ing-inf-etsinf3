using EcoScooter.Services;
using System;
using System.Windows.Forms;

namespace EcoScooter.GUI
{
    public partial class Menu_Empleat : Form

    {
        private readonly IEcoScooterService service;
        public Menu_Empleat(IEcoScooterService service)
        {
            InitializeComponent();
            this.service = service;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            service.Disconnect();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new Cas3_RegisterStation(service).ShowDialog();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            new Cas4_RegistrarScooter(service).ShowDialog();
        }
    }
}
