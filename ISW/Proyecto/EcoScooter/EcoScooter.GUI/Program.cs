using EcoScooter.Persistence;
using EcoScooter.Services;
using System;
using System.Windows.Forms;

namespace EcoScooter.GUI
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            IEcoScooterService service = new EcoScooterService(new EntityFrameworkDAL(new EcoScooterDbContext()));

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new EcoScooterApp(service));
        }


    }
}
