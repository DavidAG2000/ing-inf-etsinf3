using EcoScooter.Services;
using System;
using System.Windows.Forms;

namespace EcoScooter.GUI
{
    public partial class EcoScooterApp : Form
    {
        private readonly IEcoScooterService service;

        public EcoScooterApp(IEcoScooterService service)
        {
            InitializeComponent();
            this.service = service;
        }

        private void EcoScooterApp_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                service.LoginUser(textBox1.Text, textBox2.Text);
                this.Hide();
                new Menu_Cliente(service).ShowDialog();
                this.Show();
            }
            catch (ServiceException exc)
            {
                MessageBox.Show(exc.Message, "Error");
            }

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Registrar-se
            new Cas1_Registrarse(service).ShowDialog();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            // Entrar com empleat
            this.Hide();
            new Cas2_LoginEmployee(service).ShowDialog();
            this.Show();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
