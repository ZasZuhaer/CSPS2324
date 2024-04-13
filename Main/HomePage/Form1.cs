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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoginButton.Visible = false;
            SignUpButton.Visible = false;
            TopPanel.Visible = true;
            SignUpPanel.Visible = true;
            LoginPanel.Visible = false;
         
        }

        private void button2_Click(object sender, EventArgs e)
        {
            LoginButton.Visible = false;
            SignUpButton.Visible = false;
            TopPanel.Visible = true;
            SignUpPanel.Visible = false;
            LoginPanel.Visible = true;

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            LoginButton.Visible = true;
            SignUpButton.Visible = true;
            TopPanel.Visible = false;
            SignUpPanel.Visible = false;
            LoginPanel.Visible = false;
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            if (LoginUsingEmailButton.Text == "Login using Email")
            {
                LoginUsingEmailButton.Text = "Login using Username";
                LoginUsernameLabel.Text = "Email:";
            }
            else
            {
                LoginUsingEmailButton.Text = "Login using Email";
                LoginUsernameLabel.Text = "Username:";
            }
        }

        private void LoginPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }
    }
}
