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
            AccountSettings accountSettings = new AccountSettings(brokerEmail);
            accountSettings.ShowDialog();
            
            

        }

        private void button3_Click(object sender, EventArgs e)
        {
            SeeAddedTasks();
            
        }

        void SeeAddedTasks()
        {

            List<TaskInfo> tasks = ServerFunctions.SeeAddedTasks(brokerEmail);
            flowLayoutPanel1.Controls.Clear();
            foreach (TaskInfo task in tasks)
            {
                TaskBoxControl taskBox = new TaskBoxControl(brokerEmail, "Broker");
                taskBox.SetTaskDetails(task);
                flowLayoutPanel1.Controls.Add(taskBox);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            SeePendingTasks();
        }
        void SeePendingTasks()
        {

            List<TaskInfo> tasks = ServerFunctions.SeePendingTasks(brokerEmail);
            flowLayoutPanel1.Controls.Clear();

            foreach (TaskInfo task in tasks)
            {
                TaskBoxControl taskBox = new TaskBoxControl(brokerEmail, "Broker");
                taskBox.SetTaskDetails(task);
                flowLayoutPanel1.Controls.Add(taskBox);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string searchStr = textBox1.Text;
            List<TaskInfo> tasks = ServerFunctions.SearchTasksBySubstring(searchStr);
            flowLayoutPanel1.Controls.Clear();

            foreach (TaskInfo task in tasks)
            {
                TaskBoxControl taskBox = new TaskBoxControl(brokerEmail, "Broker");
                taskBox.SetTaskDetails(task);
                flowLayoutPanel1.Controls.Add(taskBox);
            }
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
