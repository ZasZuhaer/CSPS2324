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
    public partial class Form3 : Form
    {
        string email;
        public Form3(string email)
        {
            this.email = email;
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string searchStr = textBox1.Text;
            List<TaskInfo> tasks = ServerFunctions.SearchTasksBySubstring(searchStr);
            flowLayoutPanel1.Controls.Clear();

            foreach (TaskInfo task in tasks)
            {
                TaskBoxControl taskBox = new TaskBoxControl(email, "Client");
                taskBox.SetTaskDetails(task);
                flowLayoutPanel1.Controls.Add(taskBox);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ClientAccountSettings clientAccountSettings = new ClientAccountSettings(email);
            clientAccountSettings.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<TaskInfo> tasks = ServerFunctions.SeeActiveTasks();
            flowLayoutPanel1.Controls.Clear();
            foreach (TaskInfo task in tasks)
            {
                TaskBoxControl taskBox = new TaskBoxControl(email, "Client");
                taskBox.SetTaskDetails(task);
                flowLayoutPanel1.Controls.Add(taskBox);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            List<TaskInfo> tasks = ServerFunctions.SeeOngoingTasks(email);
            flowLayoutPanel1.Controls.Clear();
            foreach (TaskInfo task in tasks)
            {
                TaskBoxControl taskBox = new TaskBoxControl(email, "Client");
                taskBox.SetTaskDetails(task);
                flowLayoutPanel1.Controls.Add(taskBox);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            List<TaskInfo> tasks = ServerFunctions.SeeUnpaidTasks(email);
            flowLayoutPanel1.Controls.Clear();
            foreach (TaskInfo task in tasks)
            {
                TaskBoxControl taskBox = new TaskBoxControl(email, "Client");
                taskBox.SetTaskDetails(task);
                flowLayoutPanel1.Controls.Add(taskBox);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
