using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace HomePage
{
    public partial class AddTask : Form
    {
        string brokerName;
        string brokerEmail;
        public AddTask(string brokerName, string brokerEmail)
        {
            this.brokerName = brokerName;
            this.brokerEmail = brokerEmail;
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(comboBox1.Text) ||
                string.IsNullOrWhiteSpace(comboBox2.Text) ||
                string.IsNullOrWhiteSpace(comboBox3.Text) ||
                string.IsNullOrWhiteSpace(textBox1.Text) ||
                string.IsNullOrWhiteSpace(textBox2.Text))
            {
                // If any control is empty, return false
                TaskVerificationMsg.Text = "Please insert all the informations";
            }
            else
            {
                bool isDuplicate = ServerFunctions.checkTask(comboBox1.Text, comboBox2.Text, comboBox3.Text, textBox1.Text, textBox2.Text, brokerName);
                if (isDuplicate)
                {
                    TaskVerificationMsg.Text = "Task already added before!";
                }
                else
                {
                    ServerFunctions.addTask(comboBox1.Text, comboBox2.Text, comboBox3.Text,
                        textBox1.Text, textBox2.Text, brokerName, brokerEmail);
                    TaskVerificationMsg.Text = "Task added successfully!";
                    comboBox1.Text = "";
                    comboBox2.Text = "";
                    comboBox3.Text = "";
                    textBox1.Text = "";
                    textBox2.Text = "";
                }
                
            }
        
        }

        private void AddTask_Load(object sender, EventArgs e)
        {

        }

        private void DataVerificationMsg_Click(object sender, EventArgs e)
        {

        }
    }
}
