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
    public partial class AdminControl : Form
    {
        public AdminControl()
        {
            InitializeComponent();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

            LoadUsers();


        }
        private void LoadUsers()
        {
            button4.Text = "Delete User";
            button4.Show();
            dataGridView1.Hide();
            dataGridView2.Show();
            this.usersTableAdapter.Fill(this.theDatabaseDataSet1.Users);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            LoadTasks();

        }
        private void LoadTasks()
        {
            button4.Text = "Delete Task";
            button4.Show();

            dataGridView1.Show();
            dataGridView2.Hide();
            this.tasksTableAdapter.Fill(this.theDatabaseDataSet.Tasks);
        }

        private void AdminControl_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'theDatabaseDataSet1.Users' table. You can move, or remove it, as needed.
            // TODO: This line of code loads data into the 'theDatabaseDataSet.Tasks' table. You can move, or remove it, as needed.

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (button4.Text == "Delete Task")
                {
                    int selectedTaskId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
                    ServerFunctions.DeleteTask(selectedTaskId);
                    LoadTasks();
                }
                else if (button4.Text == "Delete User")
                {
                    int selectedUserId = Convert.ToInt32(dataGridView2.SelectedRows[0].Cells[0].Value);
                    if (selectedUserId == 1093)
                    {
                        MessageBox.Show("Admin user cannot be deleted!");
                    }
                    else
                    {
                        ServerFunctions.DeleteUser(selectedUserId);
                    }
                    LoadUsers();
                }
            }
            catch {
                MessageBox.Show("Select rows from the left black boxes of each row");
            }



        }
    }
}
