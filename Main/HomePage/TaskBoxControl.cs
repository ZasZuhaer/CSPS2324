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
        public TaskBoxControl()
        {
            InitializeComponent();
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
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void TaskBoxControl_Load(object sender, EventArgs e)
        {

        }
    }
}
