using EcoScooter.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace EcoScooter.GUI
{
    public partial class Cas8_GetRoutes : Form
    {
        private readonly IEcoScooterService service;
        public Cas8_GetRoutes(IEcoScooterService service)
        {
            InitializeComponent();
            this.service = service;
        }

        private void addPersonButton_Click(object sender, EventArgs e)
        {
            try
            {
                reservationsdataGridView.Rows.Clear();
                DateTime dt1 = driverLicensedateTimePicker.Value;
                List<String> aux = service.GetUserRoutes(driverLicensedateTimePicker.Value, dateTimePicker1.Value).ToList();
                foreach (String r in aux)
                {
                    reservationsdataGridView.Rows.Add(r.Split(','));
                }

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

        private void reservationsdataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
