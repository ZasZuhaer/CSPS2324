using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace HomePage
{
    public partial class TaskBoxControl : UserControl
    {
        string accountType;
        string email;
        string brokerEmail;
        int taskID;
        //string brokerRating;
        public TaskBoxControl(string email, string type)
        {
            this.email = email;
            accountType = type;
            InitializeComponent();
            if(accountType == "Broker")
            {
                button1.Enabled = false;
                button2.Enabled = false;
                button2.Hide();
                button1.Hide();

            }
            
        }

        public void SetTaskDetails(TaskInfo task)
        {
            label9.Text = task.Office;
            label8.Text = task.Name;
            label10.Text = task.Type;
            label11.Text = task.CompletionTime;
            label12.Text = task.Broker;
            label13.Text = (task.BidPrice != null ? task.BidPrice.ToString() : "Not specified");
            textBox2.Text = task.Description;
            label15.Text = Convert.ToString(task.Id);
            label17.Text = "Your balance " + ServerFunctions.getBalance(email);
            button3.Text = "Pay " + task.BidPrice;
            label17.Hide();
            button3.Hide();
            button4.Hide();
            brokerEmail = task.Email;
            taskID = task.Id;
            
            if(task.Status == "active" && task.Payment == email)
            {
                label17.Show();
                button3.Show();
            }
            if(task.Status == "Pending" && accountType == "Broker")
            {
                button4.Text = "Mark Completed!";
                button4.Show();
            }
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void TaskBoxControl_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string taskID = label15.Text;
            bool isSuccess = ServerFunctions.ChangeTaskStatusToPending(Convert.ToInt32(taskID));
            if (!isSuccess)
            {
                MessageBox.Show("Couldn't change status to pending");
            }
            isSuccess = ServerFunctions.ChangePaymentToClientEmail(Convert.ToInt32(taskID), email);
            if (!isSuccess)
            {
                MessageBox.Show("Couldn't change payment status to email");
            }
            else
            {
                MessageBox.Show("Task Requested!");
            }
        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            int mainAmount = Convert.ToInt32(label13.Text);
            int commission = mainAmount>100?10:5;
            int brokerGets = mainAmount - commission;

            ServerFunctions.DeductBalance(email, mainAmount);
            ServerFunctions.DepositBalance(brokerEmail, brokerGets);
            ServerFunctions.DepositBalance("admin@govtaid.com", commission);
            ServerFunctions.ChangePaymentStatusToPaid(taskID);
            MessageBox.Show("Payment Successful! Please Refresh");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ServerFunctions.ChangeTaskStatusToActive(Convert.ToInt32(taskID));
            MessageBox.Show("Task Marked as Completed! Please refresh to update screen");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }
    }
}
