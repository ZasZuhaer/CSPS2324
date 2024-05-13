﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Drawing.Text;


namespace HomePage
{
    internal class ServerFunctions
    {
        internal static SqlConnection TheDatabase;

        internal static void CreateSqlConnection()
        {
            TheDatabase = new SqlConnection("Data Source=(local);Initial Catalog=TheDatabase;Integrated Security=True");
            TheDatabase.Open();
            //MessageBox.Show("Connection Openned");
        }

        internal static void CloseSqlConnection()
        {
            TheDatabase.Close();
            //MessageBox.Show("Connection Closed");
        }
        internal static bool AddUser(string name, string email, string password, string phone, string role)
        {
            CreateSqlConnection();

            string addUserCommand = "INSERT INTO USERS(Name, Email, Password, Phone, Role) " +
                                    $"VALUES('{name}', '{email}', '{password}', '{phone}', '{role}')";

            SqlCommand addUser = new SqlCommand(addUserCommand, TheDatabase);
            
            int rows = 0;  
            try
            {
                rows = addUser.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                if (e.Message.Contains("@"))
                {
                    HomePage.Form1.DataVerificationMsg.Text = "Email already exists";
                }
                else
                {
                    HomePage.Form1.DataVerificationMsg.Text = "Phone number already exists";
                }
            }
            
            CloseSqlConnection();

            return (rows > 0) ;
        }

        internal static bool isExistingAccount(string email)
        {
            CreateSqlConnection();

            string countCommand = $"SELECT COUNT(*) FROM USERS WHERE EMAIL = '{email}'";
            SqlCommand countQuery = new SqlCommand(countCommand, TheDatabase);
            int rowCount = (int)countQuery.ExecuteScalar();

            CloseSqlConnection();

            return rowCount > 0;
        }
        
        internal static string getAccountType(string email)
        {
            CreateSqlConnection();

            string accountType = "";

            string checkUserCommand = "SELECT ROLE FROM USERS " +
                                      $"WHERE EMAIL = '{email}'";
            SqlCommand checkUser = new SqlCommand(checkUserCommand, TheDatabase);
            object result = checkUser.ExecuteScalar();

            if (result != null)
            {
                accountType = result.ToString();
            }

            CloseSqlConnection();

            return accountType;
        }

        internal static string getPassword(string email)
        {
            CreateSqlConnection();

            string password = "";

            string getPasswordCommand = "SELECT PASSWORD FROM USERS " +
                                        $"WHERE EMAIL = '{email}'";
            SqlCommand getPasswordCmd = new SqlCommand(getPasswordCommand, TheDatabase);
            object result = getPasswordCmd.ExecuteScalar();

            if (result != null)
            {
                password = result.ToString();
            }

            CloseSqlConnection();

            return password;
        }

        internal static bool addTask(string comboBox1Value, string comboBox2Value, string comboBox3Value, string textBox1Value, string textBox2Value, string brokerName, string brokerEmail)
        {
            CreateSqlConnection();

            string addTaskCommand = "INSERT INTO Tasks(Office, NAME, TYPE, DESCRIPTION, COMPLETIONTIME, Broker, Email) " +
                                    $"VALUES('{comboBox1Value}', '{comboBox2Value}', '{comboBox3Value}', '{textBox1Value}', '{textBox2Value}', '{brokerName}', '{brokerEmail}')";

            SqlCommand addTask = new SqlCommand(addTaskCommand, TheDatabase);

            int rows = 0;
            
            rows = addTask.ExecuteNonQuery();
            


            return (rows > 0);
        }
        
        internal static bool checkTask(string comboBox1Value, string comboBox2Value, string comboBox3Value, string textBox1Value, string textBox2Value, string brokerName)
        {
            CreateSqlConnection();

            bool isDuplicate = false;


            string query = $"SELECT COUNT(*) FROM Tasks WHERE Office = '{comboBox1Value}' AND Name = '{textBox1Value}' AND Type = '{comboBox2Value}' AND Description = '{textBox2Value}' AND CompletionTime = '{comboBox3Value}' AND Broker = '{brokerName}'";

            SqlCommand command = new SqlCommand(query, TheDatabase);
                

                int count = (int)command.ExecuteScalar();

                if (count > 0)
                {
                    isDuplicate = true;
                }
            
            

            CloseSqlConnection();

            return isDuplicate;
        }

        internal static string getName(string email)
        {
            string name = "";

            CreateSqlConnection();

            // Define the SQL query to select the name based on the email
            string query = $"SELECT Name FROM Users WHERE Email = '{email}'";

            // Create a SqlCommand object
            SqlCommand command = new SqlCommand(query, TheDatabase);

            // Execute the SQL query and retrieve the name
            object result = command.ExecuteScalar();

            // Check if the result is not null
            if (result != null)
            {
                // Cast the result to string (assuming the name is stored as a string in the database)
                name = result.ToString();
            }

            CloseSqlConnection();

            return name;
        }

    }
}
