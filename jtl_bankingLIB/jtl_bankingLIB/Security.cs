using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace jtl_bankingLIB
{
    public enum LoginType
    {
        Admin,
        Guest
    }
    public class Security
    {
        public int adminUsername { get; set; }
        public string adminPassword { get; set; }
        public string adminStatus { get; set; }
        public int adminAttempts { get; set; }


        public int customerUsername { get; set; }
        public string customerPassword { get; set; }
        public string customerAccStatus { get; set; }
        public int customerAccAttempts { get; set; }


        SqlConnection con = new SqlConnection("server=ARMSTRONG\\JACOBINSTANCE;database=bankingAdminDB;integrated security=true");

        #region Security: Check Admin Login Credentials
        public bool CheckLoginCredentials(string loginUsername, string loginUserpass, LoginType guest_type)
        {
            SqlConnection con = new SqlConnection("server=ARMSTRONG\\JACOBINSTANCE;database=bankingAdminDB;integrated security=true");
            SqlCommand cmdCheckLoginCreds;
            if (guest_type == LoginType.Admin)
            {
                cmdCheckLoginCreds = new SqlCommand("select count(*) from adminLogin where adminUsername = @Jacob and adminPassword = @Password123", con);
                
            }
            else
            {
                cmdCheckLoginCreds = new SqlCommand("select count(*) from adminLogin where adminUsername = @Jacob and adminPassword = @Password123", con);
                SqlCommand updateAdminAttempts;
                if (guest_type == LoginType.Admin && adminAttempts == 2)
                {
                    updateAdminAttempts = new SqlCommand("update adminLogin set adminStatus = 'Locked' where adminUsername = @adminUsername", con);
                }
                else
                {
                    updateAdminAttempts = new SqlCommand("update adminLogin set adminAttempts = adminAttempts + 1 where adminUserName = @adminUsername", con);
                }
                updateAdminAttempts.Parameters.AddWithValue("@adminUsername", loginUsername);//breakpoint
                con.Open();
                updateAdminAttempts.ExecuteNonQuery();
                con.Close();

                Console.WriteLine("Admin login failed" + loginUsername);//breakpoint
            }
            cmdCheckLoginCreds.Parameters.AddWithValue("@Jacob", loginUsername);
            cmdCheckLoginCreds.Parameters.AddWithValue("@Password123", loginUserpass);

            con.Open();
            int checkResult = Convert.ToInt32(cmdCheckLoginCreds.ExecuteScalar());
            con.Close();
            if (checkResult == 0)
            {
                Console.WriteLine("Invalid Credentials");
                return false;
            }

            if (guest_type == LoginType.Guest)
            {
                SqlCommand cmdCustomerAttempts = new SqlCommand("select customerStatus, customerAttempts from customerLogin where customerUserName=customerUsername", con);
                cmdCustomerAttempts.Parameters.AddWithValue("@customerUsername", loginUsername);
                con.Open();

                SqlDataReader readUser = cmdCustomerAttempts.ExecuteReader();
                if (readUser.Read())
                {
                    string customerStatus = readUser[0].ToString();
                    int customerAttempts = Convert.ToInt32(readUser[1].ToString());
                    readUser.Close();

                    if (customerStatus == "Locked")
                    {
                        Console.WriteLine("Account has been locked due to security reasons. Please contact customer support.");
                    }
                    if (checkResult == 1 && customerStatus == "Active")
                    {
                        Console.WriteLine("Login Successful. Please Wait.");
                    }
                    else
                    {
                        SqlCommand updateAttempts;
                        if (guest_type == LoginType.Guest && customerAttempts == 2)
                        {
                            updateAttempts = new SqlCommand("update customerLogin set customerStatus = 'Locked' where customerUsername = @customerUsername", con);
                        }
                        else
                        {
                            updateAttempts = new SqlCommand("update customerLogin set customerAttempts = customerAttempts + 1 where customerUserName = @customerUserName", con);
                        }
                        updateAttempts.Parameters.AddWithValue("@customerUsername", loginUsername);
                        updateAttempts.ExecuteNonQuery();

                        Console.WriteLine("User login failed" + loginUsername);
                    }
                }
                con.Close();
            }
            else
            { 
                if (checkResult == 1 && adminStatus == "Active")
                {
                    Console.WriteLine("Admin login successful. Please Wait.");
                }
                else
                {
                    Console.WriteLine("Admin login successful!");
                }
            }
            return true;
        }
        #endregion

        #region Security: Check Customer Login Credentials
        public bool CheckCustomerCredentials(string customer_name, string customer_pass)
        {
            SqlCommand cmdCheckCustomerLogin = new SqlCommand("select count(*) from customerLogin where customerUsername = @customerUsername and customerPassword = @customerPassword", con);
            cmdCheckCustomerLogin.Parameters.AddWithValue("@customerUsername", customer_name);
            cmdCheckCustomerLogin.Parameters.AddWithValue("@customerPassword", customer_pass);
            con.Open();
            int customerResult = Convert.ToInt32(cmdCheckCustomerLogin.ExecuteScalar());
            con.Close();
            if (customerResult == 0)
            {
                return false;
            }
            return true;
        }
        #endregion


    }
}
