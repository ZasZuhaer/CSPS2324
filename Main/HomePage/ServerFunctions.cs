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
    public class TaskInfo
    {
        public int Id { get; set; }
        public string Office { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public string CompletionTime { get; set; }
        public string Broker { get; set; }
        public string Email { get; set; }
        public string BidPrice { get; set; } // Nullable

        public string Status {  get; set; }
        public string Payment {  get; set; }
    }

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
                    //HomePage.Form1.DataVerificationMsg.Text = "Email already exists";
                }
                else
                {
                    //HomePage.Form1.DataVerificationMsg.Text = "Phone number already exists";
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

        internal static bool addTask(string comboBox1Value, string comboBox2Value, string comboBox3Value, string textBox1Value, string textBox2Value, string brokerName, string brokerEmail, string price)
        {
            CreateSqlConnection();

            string addTaskCommand = "INSERT INTO Tasks(Office, NAME, TYPE, DESCRIPTION, COMPLETIONTIME, Broker, Email, BidPrice) " +
                                    $"VALUES('{comboBox1Value}', '{textBox1Value}', '{comboBox2Value}', '{textBox2Value}', '{comboBox3Value}', '{brokerName}', '{brokerEmail}', '{price}')";

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
        internal static string getBalance(string email)
        {
            string name = "";

            CreateSqlConnection();

            // Define the SQL query to select the name based on the email
            string query = $"SELECT Balance FROM Users WHERE Email = '{email}'";

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

        internal static bool DeductBalance(string email, decimal amount)
        {
            // Create the SQL connection
            CreateSqlConnection();

            // Define the SQL command to deduct the amount from the user's balance
            string deductCommand = $"UPDATE Users SET Balance = Balance - {amount} WHERE Email = '{email}'";

            // Create a SqlCommand object
            SqlCommand deductCmd = new SqlCommand(deductCommand, TheDatabase);

            // Execute the SQL command
            int rowsAffected = deductCmd.ExecuteNonQuery();

            // Close the SQL connection
            CloseSqlConnection();

            // Return true if rows were affected (indicating successful deduction), otherwise false
            return rowsAffected > 0;
        }

        internal static List<TaskInfo> GetAllTasks()
        {
            List<TaskInfo> tasks = new List<TaskInfo>();

            // Create SQL connection
            CreateSqlConnection();

            // Define SQL command to select all tasks
            string query = "SELECT * FROM Tasks";

            // Create SqlCommand object
            SqlCommand command = new SqlCommand(query, TheDatabase);

            // Execute the command
            using (SqlDataReader reader = command.ExecuteReader())
            {
                // Check if there are any rows
                if (reader.HasRows)
                {
                    // Iterate through the rows
                    while (reader.Read())
                    {
                        // Create a new TaskInfo object and populate its properties
                        TaskInfo task = new TaskInfo
                        {
                            Id = reader.GetInt32(0), // Assuming Id is the first column
                            Office = reader.GetString(1),
                            Name = reader.GetString(2),
                            Type = reader.GetString(3),
                            Description = reader.GetString(4),
                            CompletionTime = reader.GetString(5),
                            Broker = reader.GetString(6),
                            Email = reader.GetString(7),
                            BidPrice = reader.IsDBNull(8) ? null : reader.GetString(8), // Check for null value in BidPrice
                            Status = reader.GetString(9),
                            Payment = reader.GetString(10)
                        };

                        // Add the task to the list
                        tasks.Add(task);
                    }
                }
            }

            // Close SQL connection
            CloseSqlConnection();

            // Return the list of tasks
            return tasks;
        }

        internal static List<TaskInfo> SeeAddedTasks(string email)
        {
            List<TaskInfo> tasks = new List<TaskInfo>();

            // Create SQL connection
            CreateSqlConnection();

            // Define SQL command to select all tasks
            string query = $"SELECT * FROM Tasks WHERE Email = '{email}'";

            // Create SqlCommand object
            SqlCommand command = new SqlCommand(query, TheDatabase);

            // Execute the command
            using (SqlDataReader reader = command.ExecuteReader())
            {
                // Check if there are any rows
                if (reader.HasRows)
                {
                    // Iterate through the rows
                    while (reader.Read())
                    {
                        // Create a new TaskInfo object and populate its properties
                        TaskInfo task = new TaskInfo
                        {
                            Id = reader.GetInt32(0), // Assuming Id is the first column
                            Office = reader.GetString(1),
                            Name = reader.GetString(2),
                            Type = reader.GetString(3),
                            Description = reader.GetString(4),
                            CompletionTime = reader.GetString(5),
                            Broker = reader.GetString(6),
                            Email = reader.GetString(7),
                            BidPrice = reader.IsDBNull(8) ? null : reader.GetString(8), // Check for null value in BidPrice
                            Status = reader.GetString(9),
                            Payment = reader.GetString(10)
                        };

                        // Add the task to the list
                        tasks.Add(task);
                    }
                }
            }

            // Close SQL connection
            CloseSqlConnection();

            // Return the list of tasks
            return tasks;
        }

        internal static List<TaskInfo> SeePendingTasks(string email)
        {
            List<TaskInfo> tasks = new List<TaskInfo>();

            // Create SQL connection
            CreateSqlConnection();

            // Define SQL command to select all tasks
            string query = $"SELECT * FROM Tasks WHERE Email = '{email}' and Status = 'pending'";

            // Create SqlCommand object
            SqlCommand command = new SqlCommand(query, TheDatabase);

            // Execute the command
            using (SqlDataReader reader = command.ExecuteReader())
            {
                // Check if there are any rows
                if (reader.HasRows)
                {
                    // Iterate through the rows
                    while (reader.Read())
                    {
                        // Create a new TaskInfo object and populate its properties
                        TaskInfo task = new TaskInfo
                        {
                            Id = reader.GetInt32(0), // Assuming Id is the first column
                            Office = reader.GetString(1),
                            Name = reader.GetString(2),
                            Type = reader.GetString(3),
                            Description = reader.GetString(4),
                            CompletionTime = reader.GetString(5),
                            Broker = reader.GetString(6),
                            Email = reader.GetString(7),
                            BidPrice = reader.IsDBNull(8) ? null : reader.GetString(8), // Check for null value in BidPrice
                            Status = reader.GetString(9),
                            Payment = reader.GetString(10)
                        };

                        // Add the task to the list
                        tasks.Add(task);
                    }
                }
            }

            // Close SQL connection
            CloseSqlConnection();

            // Return the list of tasks
            return tasks;
        }

        internal static List<TaskInfo> SeeActiveTasks()
        {
            List<TaskInfo> tasks = new List<TaskInfo>();

            // Create SQL connection
            CreateSqlConnection();

            // Define SQL command to select all tasks
            string query = $"SELECT * FROM Tasks WHERE Status = 'active'";

            // Create SqlCommand object
            SqlCommand command = new SqlCommand(query, TheDatabase);

            // Execute the command
            using (SqlDataReader reader = command.ExecuteReader())
            {
                // Check if there are any rows
                if (reader.HasRows)
                {
                    // Iterate through the rows
                    while (reader.Read())
                    {
                        // Create a new TaskInfo object and populate its properties
                        TaskInfo task = new TaskInfo
                        {
                            Id = reader.GetInt32(0), // Assuming Id is the first column
                            Office = reader.GetString(1),
                            Name = reader.GetString(2),
                            Type = reader.GetString(3),
                            Description = reader.GetString(4),
                            CompletionTime = reader.GetString(5),
                            Broker = reader.GetString(6),
                            Email = reader.GetString(7),
                            BidPrice = reader.IsDBNull(8) ? null : reader.GetString(8), // Check for null value in BidPrice
                            Status = reader.GetString(9),
                            Payment = reader.GetString(10)
                        };

                        // Add the task to the list
                        tasks.Add(task);
                    }
                }
            }

            // Close SQL connection
            CloseSqlConnection();

            // Return the list of tasks
            return tasks;
        }

        internal static string getId(string email)
        {
            string id = "";

            CreateSqlConnection();

            string query = $"SELECT Id FROM Users WHERE Email = '{email}'";

            SqlCommand command = new SqlCommand(query, TheDatabase);

            object result = command.ExecuteScalar();

            if (result != null)
            {
                id = result.ToString();
            }

            CloseSqlConnection();

            return id;
        }


        internal static string getPhone(string email)
        {
            string phone = "";

            CreateSqlConnection();

            string query = $"SELECT Phone FROM Users WHERE email = '{email}'";

            SqlCommand command = new SqlCommand(query, TheDatabase);

            object result = command.ExecuteScalar();

            if (result != null)
            {
                phone = result.ToString();
            }

            CloseSqlConnection();

            return phone;
        }

        internal static List<TaskInfo> SeeUnpaidTasks(string clientEmail)
        {
            List<TaskInfo> tasks = new List<TaskInfo>();

            // Create SQL connection
            CreateSqlConnection();

            // Define SQL command to select all tasks
            string query = $"SELECT * FROM Tasks WHERE status = 'active' and payment = '{clientEmail}'";

            // Create SqlCommand object
            SqlCommand command = new SqlCommand(query, TheDatabase);

            // Execute the command
            using (SqlDataReader reader = command.ExecuteReader())
            {
                // Check if there are any rows
                if (reader.HasRows)
                {
                    // Iterate through the rows
                    while (reader.Read())
                    {
                        // Create a new TaskInfo object and populate its properties
                        TaskInfo task = new TaskInfo
                        {
                            Id = reader.GetInt32(0), // Assuming Id is the first column
                            Office = reader.GetString(1),
                            Name = reader.GetString(2),
                            Type = reader.GetString(3),
                            Description = reader.GetString(4),
                            CompletionTime = reader.GetString(5),
                            Broker = reader.GetString(6),
                            Email = reader.GetString(7),
                            BidPrice = reader.IsDBNull(8) ? null : reader.GetString(8), // Check for null value in BidPrice
                            Status = reader.GetString(9),
                            Payment = reader.GetString(10)
                        };

                        // Add the task to the list
                        tasks.Add(task);
                    }
                }
            }

            // Close SQL connection
            CloseSqlConnection();

            // Return the list of tasks
            return tasks;
        }

        internal static List<TaskInfo> SeeOngoingTasks(string clientEmail)
        {
            List<TaskInfo> tasks = new List<TaskInfo>();

            // Create SQL connection
            CreateSqlConnection();

            // Define SQL command to select all tasks
            string query = $"SELECT * FROM Tasks WHERE payment = '{clientEmail}'";

            // Create SqlCommand object
            SqlCommand command = new SqlCommand(query, TheDatabase);

            // Execute the command
            using (SqlDataReader reader = command.ExecuteReader())
            {
                // Check if there are any rows
                if (reader.HasRows)
                {
                    // Iterate through the rows
                    while (reader.Read())
                    {
                        // Create a new TaskInfo object and populate its properties
                        TaskInfo task = new TaskInfo
                        {
                            Id = reader.GetInt32(0), // Assuming Id is the first column
                            Office = reader.GetString(1),
                            Name = reader.GetString(2),
                            Type = reader.GetString(3),
                            Description = reader.GetString(4),
                            CompletionTime = reader.GetString(5),
                            Broker = reader.GetString(6),
                            Email = reader.GetString(7),
                            BidPrice = reader.IsDBNull(8) ? null : reader.GetString(8), // Check for null value in BidPrice
                            Status = reader.GetString(9),
                            Payment = reader.GetString(10)
                        };

                        // Add the task to the list
                        tasks.Add(task);
                    }
                }
            }

            // Close SQL connection
            CloseSqlConnection();

            // Return the list of tasks
            return tasks;
        }
        internal static string getAddress(string email)
        {
            string address = "";

            CreateSqlConnection();

            string query = $"SELECT Address FROM Users WHERE email = '{email}'";

            SqlCommand command = new SqlCommand(query, TheDatabase);

            object result = command.ExecuteScalar();

            if (result != null)
            {
                address = result.ToString();
            }

            CloseSqlConnection();

            return address;
        }

        internal static string getRatings(string email)
        {
            string ratings = "";

            CreateSqlConnection();

            string query = $"SELECT Rating FROM Users WHERE email = '{email}'";

            SqlCommand command = new SqlCommand(query, TheDatabase);

            object result = command.ExecuteScalar();

            if (result != null)
            {
                ratings = result.ToString();
            }

            CloseSqlConnection();

            return ratings;
        }

        internal static List<TaskInfo> SearchTasksBySubstring(string substring)
        {
            List<TaskInfo> matchingTasks = new List<TaskInfo>();

            CreateSqlConnection();

            string query = $@"
        SELECT *
        FROM Tasks
        WHERE Name LIKE '%{substring}%'
            OR Description LIKE '%{substring}%'
            OR Office LIKE '%{substring}%'
            OR Type LIKE '%{substring}%'
            OR Broker LIKE '%{substring}%';";

            SqlCommand command = new SqlCommand(query, TheDatabase);

            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        TaskInfo task = new TaskInfo
                        {
                            Id = reader.GetInt32(0),
                            Office = reader.GetString(1),
                            Name = reader.GetString(2),
                            Type = reader.GetString(3),
                            Description = reader.GetString(4),
                            CompletionTime = reader.GetString(5),
                            Broker = reader.GetString(6),
                            Email = reader.GetString(7),
                            BidPrice = reader.IsDBNull(8) ? null : reader.GetString(8),
                            Status = reader.GetString(9),
                            Payment = reader.GetString(10)
                        };

                        matchingTasks.Add(task);
                    }
                }
            }

            CloseSqlConnection();

            return matchingTasks;
        }

        internal static bool DepositBalance(string email, decimal amount)
        {
            // Create the SQL connection
            CreateSqlConnection();

            // Define the SQL command to deduct the amount from the user's balance
            string deductCommand = $"UPDATE Users SET Balance = Balance + {amount} WHERE Email = '{email}'";

            // Create a SqlCommand object
            SqlCommand deductCmd = new SqlCommand(deductCommand, TheDatabase);

            // Execute the SQL command
            int rowsAffected = deductCmd.ExecuteNonQuery();

            // Close the SQL connection
            CloseSqlConnection();

            // Return true if rows were affected (indicating successful deduction), otherwise false
            return rowsAffected > 0;
        }

        internal static bool ChangeTaskStatusToPending(int taskId)
        {
            CreateSqlConnection();

            string updateCommand = $"UPDATE Tasks SET Status = 'Pending' WHERE Id = {taskId}";

            SqlCommand updateTask = new SqlCommand(updateCommand, TheDatabase);

            int rowsAffected = updateTask.ExecuteNonQuery();

            CloseSqlConnection();

            return rowsAffected > 0;
        }

        internal static bool ChangeTaskStatusToActive(int taskId)
        {
            CreateSqlConnection();

            string updateCommand = $"UPDATE Tasks SET Status = 'active' WHERE Id = {taskId}";

            SqlCommand updateTask = new SqlCommand(updateCommand, TheDatabase);

            int rowsAffected = updateTask.ExecuteNonQuery();

            CloseSqlConnection();

            return rowsAffected > 0;
        }

        internal static bool ChangePaymentStatusToPaid(int taskId)
        {
            CreateSqlConnection();

            string updateCommand = $"UPDATE Tasks SET payment = 'paid' WHERE Id = {taskId}";

            SqlCommand updateTask = new SqlCommand(updateCommand, TheDatabase);

            int rowsAffected = updateTask.ExecuteNonQuery();

            CloseSqlConnection();

            return rowsAffected > 0;
        }

        internal static bool ChangePaymentToClientEmail(int taskId, string clientEmail)
        {
            CreateSqlConnection();

            string updateCommand = $"UPDATE Tasks SET Payment = '{clientEmail}' WHERE Id = {taskId}";

            SqlCommand updateTask = new SqlCommand(updateCommand, TheDatabase);

            int rowsAffected = updateTask.ExecuteNonQuery();

            CloseSqlConnection();

            return rowsAffected > 0;
        }

        internal static void DeleteUser(int userId)
        {
            using (SqlConnection connection = new SqlConnection("Data Source=(local);Initial Catalog=TheDatabase;Integrated Security=True"))
            {
                connection.Open();
                string deleteQuery = $"DELETE FROM Users WHERE id = @UserId";
                SqlCommand command = new SqlCommand(deleteQuery, connection);
                command.Parameters.AddWithValue("@UserId", userId);
                command.ExecuteNonQuery();
            }
        }

        internal static void DeleteTask(int taskId)
        {
            using (SqlConnection connection = new SqlConnection("Data Source=(local);Initial Catalog=TheDatabase;Integrated Security=True"))
            {
                connection.Open();
                string deleteQuery = $"DELETE FROM Tasks WHERE Id = @TaskId";
                SqlCommand command = new SqlCommand(deleteQuery, connection);
                command.Parameters.AddWithValue("@TaskId", taskId);
                command.ExecuteNonQuery();
            }
        }
    }
}
