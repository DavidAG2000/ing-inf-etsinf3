using EcoScooter.Services;
using System;
using System.Windows.Forms;

namespace EcoScooter.GUI
{
    public partial class Menu_Cliente : Form
    {
        private readonly IEcoScooterService service;
        public Menu_Cliente(IEcoScooterService service)
        {
            InitializeComponent();
            this.service = service;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            service.Disconnect();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new Cas8_GetRoutes(service).ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new Cas5_RentScooter(service).ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new Cas6_ReturnScooter(service).ShowDialog();
        }
    }
}
