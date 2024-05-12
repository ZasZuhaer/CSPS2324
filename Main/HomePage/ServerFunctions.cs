using System;
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

    }
}
