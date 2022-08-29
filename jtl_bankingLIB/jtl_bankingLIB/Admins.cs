using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace jtl_bankingLIB
{
    public class Admins
    {
        public int adminNo { get; set; }
        public string adminName { get; set; }
        public string adminType { get; set; }
        public string adminStatus { get; set; }

        public int customerNo { get; set; }
        public string customerName { get; set; }
        public string customerAccType { get; set; }
        public int customerAccCheckBalance { get; set; }
        public int customerAccSaveBalance { get; set; }
        public int customerAccLoanBalance { get; set; }
        public string customerAccStatus { get; set; }
        public int Amount { get; set; }

        SqlConnection con = new SqlConnection("server=ARMSTRONG\\JACOBINSTANCE;database=bankingAdminDB;integrated security=true");

        #region Admin: Open New Account
        public string OpenAccount(Admins p_accObj)
        {
            SqlConnection con = new SqlConnection("server=ARMSTRONG\\JACOBINSTANCE;database=bankingAdminDB;integrated security=true");
            SqlCommand cmdNewAccount = new SqlCommand("insert into customerAccounts values(@customerNo, @customerName, @customerAccType, @customerAccCheckBalance, @customerAccSaveBalance, @customerAccLoanBalance, @customerAccStatus)", con);
            cmdNewAccount.Parameters.AddWithValue("@customerNo", p_accObj.customerNo);
            cmdNewAccount.Parameters.AddWithValue("@customerName", p_accObj.customerName);
            cmdNewAccount.Parameters.AddWithValue("@customerAccType", p_accObj.customerAccType);
            cmdNewAccount.Parameters.AddWithValue("@customerAccCheckBalance", p_accObj.customerAccCheckBalance);
            cmdNewAccount.Parameters.AddWithValue("@customerAccSaveBalance", p_accObj.customerAccSaveBalance);
            cmdNewAccount.Parameters.AddWithValue("@customerAccLoanBalance", p_accObj.customerAccLoanBalance);
            cmdNewAccount.Parameters.AddWithValue("@customerAccStatus", p_accObj.customerAccStatus);
            con.Open();
            cmdNewAccount.ExecuteNonQuery();
            con.Close();
            return "Account has been added successfully";
        }
        #endregion

        #region Admin: Retrieve All Accounts
        public List<Admins> GetAllAccounts()
        {
            SqlConnection con = new SqlConnection("server=ARMSTRONG\\JACOBINSTANCE;database=bankingAdminDB;integrated security=true");
            SqlCommand cmdAllAccounts = new SqlCommand("select * from customerAccounts order by customerNo", con);
            con.Open();
            SqlDataReader readAccounts = cmdAllAccounts.ExecuteReader();
            List<Admins> accountList = new List<Admins>();
            while (readAccounts.Read())
            {
                accountList.Add(new Admins()
                {
                    customerNo = (int)readAccounts[0],
                    customerName = readAccounts[1].ToString(),
                    customerAccType = readAccounts[2].ToString(),
                    customerAccStatus = readAccounts[3].ToString(),
                });
            }
            readAccounts.Close();
            con.Close();
            return accountList;
        }
        #endregion

        #region Admin: Inititate Withdrawal
        public double Withdraw(double wAmount)
        {
            SqlConnection con = new SqlConnection("server=ARMSTRONG\\JACOBINSTANCE;database=bankingAdminDB;integrated security=true");
            SqlCommand cmdWithdraw = new SqlCommand("update customerAccounts set customerAccCheckBalance = customerAccCheckBalance - @Amount where customerName = customerName", con);
            cmdWithdraw.Parameters.AddWithValue("@Amount", wAmount);
            con.Open();
            cmdWithdraw.ExecuteScalar();
            con.Close();
            Console.WriteLine("Withdrawal successful");
            return wAmount;
        }
        #endregion

        #region Admin: Inititate Deposit
        public double Deposit(double dAmount)
        {
            SqlConnection con = new SqlConnection("server=ARMSTRONG\\JACOBINSTANCE;database=bankingAdminDB;integrated security=true");
            SqlCommand cmdDeposit = new SqlCommand("update customerAccounts set customerAccCheckBalance = customerAccCheckBalance + @Amount where customerName = customerName", con);
            cmdDeposit.Parameters.AddWithValue("@Amount", dAmount);
            con.Open();
            cmdDeposit.ExecuteScalar();
            con.Close();
            Console.WriteLine("Deposit Successful");
            return dAmount;
        }
        #endregion

        #region Admin: Delete Account
        public string DeleteAccounts(int p_accNo)
        {
            SqlConnection con = new SqlConnection("server=ARMSTRONG\\JACOBINSTANCE;database=bankingAdminDB;integrated security=true");
            SqlCommand cmdDeleteAccounts = new SqlCommand("delete from customerAccounts where customerNo = @customerNo)", con);
            cmdDeleteAccounts.Parameters.AddWithValue("@customerNo", p_accNo);
            con.Open();
            int AccAffected = cmdDeleteAccounts.ExecuteNonQuery();
            con.Close();

            if (AccAffected == 0)
            {
                return "This account does not exist";
            }
            return "Account Deletion Successful.";
        }
        #endregion

        #region Admin: Account Search by ID
        public Admins AccountSearchById(int p_accNo)
        {
            SqlConnection con = new SqlConnection("server=ARMSTRONG\\JACOBINSTANCE;database=bankingAdminDB;integrated security=true");
            SqlCommand cmdCustomerSearchById = new SqlCommand("select * from customerAccounts where customerNo=customerNo", con);
            cmdCustomerSearchById.Parameters.AddWithValue("@customerNo", p_accNo);
            Admins customerDetail = new Admins();
            con.Open();
            SqlDataReader readCustomer = cmdCustomerSearchById.ExecuteReader();
            if (readCustomer.Read())
            {
                customerDetail.customerNo = (int)readCustomer[0];
                customerDetail.customerName = readCustomer[1].ToString();
                customerDetail.customerAccType = readCustomer[2].ToString();
                customerDetail.customerAccStatus = readCustomer[3].ToString();
            }
            else
            {
                readCustomer.Close();
                con.Close();
                throw new Exception("Account Not Found.");
            }
            readCustomer.Close();
            con.Close();
            return customerDetail;
        }
        #endregion

        #region Admin: Check If Account Exists
        public bool CheckAccountsExist(int p_accNo)
        {
            SqlConnection con = new SqlConnection("server=ARMSTRONG\\JACOBINSTANCE;database=bankingAdminDB;integrated security=true");
            SqlCommand cmdCheckAccounts = new SqlCommand("select * from customerAccounts where customerNo = @customerNo", con);
            cmdCheckAccounts.Parameters.AddWithValue("@customerNo", p_accNo);

            con.Open();
            int count = (int) cmdCheckAccounts.ExecuteNonQuery();
            con.Close();
            if (count == 1)
            {
                return false; 
            }
            return true;
        }
        #endregion

        #region Admin: Update Accounts
        public string UpdateAccounts(Admins p_accObj)
        {
            SqlConnection con = new SqlConnection("server=ARMSTRONG\\JACOBINSTANCE;database=bankingAdminDB;integrated security=true");
            SqlCommand cmdUpdateCustomer = new SqlCommand("update customerAccounts set customerNo = @customerNo, customerName = @customerName, customerAccType = @customerAccType, customerAccStatus = @customerAccStatus", con);
            cmdUpdateCustomer.Parameters.AddWithValue("@customerNo", p_accObj.customerNo);
            cmdUpdateCustomer.Parameters.AddWithValue("@customerName", p_accObj.customerName);
            cmdUpdateCustomer.Parameters.AddWithValue("@customerAccType", p_accObj.customerAccType);
            cmdUpdateCustomer.Parameters.AddWithValue("@customerAccStatus", p_accObj.customerAccStatus);
            con.Open();
            int UpdateResult = cmdUpdateCustomer.ExecuteNonQuery();
            con.Close();
            if (UpdateResult > 0)
            {
                return "Account Information Updated Successfully.";
            }
            return "Unable to find account.";

        }
        #endregion

        #region Admin: Activate Accounts
        public string ActivateAccounts(Admins p_accAct)
        {
            SqlConnection con = new SqlConnection("server=ARMSTRONG\\JACOBINSTANCE;database=bankingAdminDB;integrated security=true");
            SqlCommand cmdActivateAccount = new SqlCommand("update customerAccounts set customerAccStatus = 'Active' where customerNo = customerNo", con);
            //cmdActivateAccount.Parameters.AddWithValue("@customerName", p_accAct.customerName);
            cmdActivateAccount.Parameters.AddWithValue("@customerNo", p_accAct.customerNo);
            cmdActivateAccount.Parameters.AddWithValue("@customerAccStatus", p_accAct.customerAccStatus);
            con.Open();
            cmdActivateAccount.ExecuteNonQuery();
            con.Close();
            
            return "Account Activated successfully.";
            
        }
        #endregion
    }
}
