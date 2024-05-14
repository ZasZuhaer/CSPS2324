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
    public partial class AccountSettings : Form
    {
        string email;
        public AccountSettings(string email)
        {
            this.email = email;

            InitializeComponent();
            this.textBox1.Text = ServerFunctions.getId(email);
            this.textBox2.Text = ServerFunctions.getName(email);
            this.textBox3.Text = email;
            this.textBox4.Text = ServerFunctions.getPhone(email);
            this.textBox5.Text = ServerFunctions.getAddress(email);
            this.textBox6.Text = ServerFunctions.getRatings(email);
            this.textBox7.Text = ServerFunctions.getBalance(email);
            

        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(textBox8.Text != "" && textBox8.Text != "" && textBox10.Text!="")
            {
                int currentBalance = Convert.ToInt32(ServerFunctions.getBalance(email));
                int withdrawAmount = 0;
                try {
                    withdrawAmount = Convert.ToInt32(textBox10.Text);
                    if (withdrawAmount <= currentBalance && withdrawAmount > 0)
                    {
                        ServerFunctions.DeductBalance(email, withdrawAmount);
                        MessageBox.Show("The amount has been sent to your wallet");
                        textBox7.Text = ServerFunctions.getBalance(email);
                        textBox10.Text = "";
                        textBox8.Text = "";
                        textBox9.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("You don't have enough balance");
                    }
                }
                catch
                {
                    MessageBox.Show("Enter a numerical value");
                }
                
                

            }
            else
            {
                MessageBox.Show("Enter amount, wallet address and network");
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
