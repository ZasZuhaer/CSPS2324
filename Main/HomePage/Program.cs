using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HomePage
{

    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());

            //Form1 loginpage = new Form1();
            //loginpage.ShowDialog();

            //Form2 BrokerPage = new Form2("zas.zuhaer@gmail.com");
            //BrokerPage.ShowDialog();

            //AdminControl admin = new AdminControl();
            //admin.ShowDialog();

            Form3 BrokerPage = new Form3("sidd@outlook.com");
            BrokerPage.ShowDialog();

        }
    }
}
