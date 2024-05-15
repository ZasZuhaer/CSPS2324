using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Data.SqlClient;


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
            NameTextBox.Text = "";
            EmailTextBox.Text = "";
            PasswordTextBox.Text = "";
            ReTypePasswordTextBox.Text = "";
            PhoneTextBox.Text = "";
            LoginEmail.Text = "";
            LoginPassword.Text = "";
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton3.Checked = false;
            radioButton4.Checked = false;
            LoginVerificationMsg.Text = "";
            DataVerificationMsg.Text = "";
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

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

        private void button4_Click(object sender, EventArgs e)
        {
            string emailPattern = @"^[\w.-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            string passwordPattern = @"^(?=.*[A-Z])(?=.*\d).+";
            string phonePattern = @"^\d{11}$";

            if(NameTextBox.Text == "auto")
            {
                DataVerificationMsg.Text = "";

                string UserRole;
                if (radioButton1.Checked)
                {
                    UserRole = "Client";
                }
                else
                {
                    UserRole = "Broker";
                }
                if (HomePage.ServerFunctions.AddUser(NameTextBox.Text, EmailTextBox.Text, PasswordTextBox.Text, PhoneTextBox.Text, UserRole))
                {
                    DataVerificationMsg.Text = "Account Successfully Created\nPlease Login to continue";
                }
            }

            else if (NameTextBox.Text == "" || NameTextBox.Text.Length < 2 || Regex.IsMatch(NameTextBox.Text, @"\d"))
            {
                DataVerificationMsg.Text = "Please enter a valid name";
            }

            else if(!(Regex.IsMatch(EmailTextBox.Text, emailPattern)))
            {
                DataVerificationMsg.Text = "Please enter a valid email";
            }
            
            else if(PasswordTextBox.Text.Length < 8)
            {
                DataVerificationMsg.Text = "Password must contain at least 8 characters";
            }

            else if(!(Regex.IsMatch(PasswordTextBox.Text, passwordPattern)))
            {
                DataVerificationMsg.Text = "Please enter a password which contains a capital letter and a number";
            }

            else if(ReTypePasswordTextBox.Text != PasswordTextBox.Text)
            {
                DataVerificationMsg.Text = "Passwords do not match";
            }

            else if(!Regex.IsMatch(PhoneTextBox.Text, phonePattern))
            {
                DataVerificationMsg.Text = "Please enter your 11 digit phone number";
            }

            else if (!radioButton1.Checked && !radioButton2.Checked)
            {
                DataVerificationMsg.Text = "Please choose an account type";
            }

            else
            {
                DataVerificationMsg.Text = "";

                string UserRole;
                if (radioButton1.Checked)
                {
                    UserRole = "Client";
                }
                else
                {
                    UserRole = "Broker";
                }
                if (HomePage.ServerFunctions.AddUser(NameTextBox.Text, EmailTextBox.Text, PasswordTextBox.Text, PhoneTextBox.Text, UserRole))
                {
                    DataVerificationMsg.Text = "Account Successfully Created\nPlease Login to continue";
                }
            }

            

        }

        private void label2_Click_1(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string emailPattern = @"^[\w.-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            //string passwordPattern = @"^(?=.*[A-Z])(?=.*\d).+";
            //string phonePattern = @"^\d{11}$";



            if (!(Regex.IsMatch(LoginEmail.Text, emailPattern)))
            {
                LoginVerificationMsg.Text = "Please enter a valid email";
            }

            else if (LoginEmail.Text == "admin@govtaid.com" && LoginPassword.Text == "Admin1234")
            {
                this.Hide();
                AdminControl adminPage = new AdminControl();
                adminPage.FormClosed += FormClosed;
                adminPage.ShowDialog();
            }
            else if (!HomePage.ServerFunctions.isExistingAccount(LoginEmail.Text))
            {
                LoginVerificationMsg.Text = "No account was found associated with this email";

            }
            //else if (!radioButton3.Checked && !radioButton4.Checked)
            //{
            //    LoginVerificationMsg.Text = "Please choose an account type";
            //}
            else if(LoginPassword.Text != ServerFunctions.getPassword(LoginEmail.Text))
            {
                LoginVerificationMsg.Text = "Enter Correct Password";
            }

            

            else
            {
                LoginVerificationMsg.Text = "Login Successful!";
                string accountType = ServerFunctions.getAccountType(LoginEmail.Text);
                if(accountType == "Broker")
                {
                    this.Hide();
                    
                    // Create an instance of Form2 (BrokerPage)
                    Form2 BrokerPage = new Form2(LoginEmail.Text);

                    // Subscribe to the FormClosed event of Form2
                    BrokerPage.FormClosed += FormClosed;

                    // Show Form2 (BrokerPage)
                    BrokerPage.ShowDialog();

                    // Hide Form1 (current form)


                }
                else if (accountType == "Client")
                {
                    //open client page
                    this.Hide();
                    Form3 ClientPage = new Form3(LoginEmail.Text);
                    ClientPage.FormClosed += FormClosed;
                    ClientPage.ShowDialog();
                }
            }
        }
        private new void FormClosed(object sender, FormClosedEventArgs e)
        {
            // Show Form1 (current form) when Form2 (BrokerPage) is closed
            LoginButton.Visible = true;
            SignUpButton.Visible = true;
            TopPanel.Visible = false;
            SignUpPanel.Visible = false;
            LoginPanel.Visible = false;
            NameTextBox.Text = "";
            EmailTextBox.Text = "";
            PasswordTextBox.Text = "";
            ReTypePasswordTextBox.Text = "";
            PhoneTextBox.Text = "";
            LoginEmail.Text = "";
            LoginPassword.Text = "";
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton3.Checked = false;
            radioButton4.Checked = false;
            LoginVerificationMsg.Text = "";
            DataVerificationMsg.Text = "";
            this.Show();
        }
        

        private void button2_Click_1(object sender, EventArgs e)
        {

        }

        private void LoginVerificationMsg_Click(object sender, EventArgs e)
        {

        }
    }
}