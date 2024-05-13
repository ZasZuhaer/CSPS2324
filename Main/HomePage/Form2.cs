using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HomePage
{
    public partial class Form2 : Form
    {
        internal string brokerEmail;
        public Form2(string Email)
        {
            brokerEmail = Email;
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AddTask TaskWindow = new AddTask(ServerFunctions.getName(brokerEmail), brokerEmail);
            TaskWindow.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            AccountSettings accountSettings = new AccountSettings();
            accountSettings.Show();
            
            

        }
    }
}
