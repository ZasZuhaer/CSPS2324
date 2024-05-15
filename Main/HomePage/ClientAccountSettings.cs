using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace HomePage
{
    public partial class ClientAccountSettings : Form
    {
        string email;
        public ClientAccountSettings(string email)
        {
            this.email = email;

            InitializeComponent();
            this.textBox1.Text = ServerFunctions.getId(email);
            this.textBox2.Text = ServerFunctions.getName(email);
            this.textBox3.Text = email;
            this.textBox4.Text = ServerFunctions.getPhone(email);
            this.textBox5.Text = ServerFunctions.getAddress(email);
            this.textBox8.Text = "bcfr7sAHTD9CgdQo3HTMTkV8LK4ZnX71";
            this.textBox7.Text = ServerFunctions.getBalance(email);


        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel1.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox10.Text != "" && textBox9.Text != "")
            {
                int depositAmount = 0;
                try
                {
                    depositAmount = Convert.ToInt32(textBox10.Text);
                    if (depositAmount >=50)
                    {
                        ServerFunctions.DepositBalance(email, depositAmount);
                        MessageBox.Show("The amount has been credited in");
                        textBox7.Text = ServerFunctions.getBalance(email);
                        textBox10.Text = "";
                        textBox9.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("Amount must be at least 50");
                    }
                }
                catch
                {
                    MessageBox.Show("Enter a numerical value");
                }



            }
            else
            {
                MessageBox.Show("Enter amount and TrxID");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
