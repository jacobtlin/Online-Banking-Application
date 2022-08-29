using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace jtl_bankingLIB
{
    public class Guests
    {
        public int guestNo { get; set; }
        public string guestName { get; set; }
        public int guestCheckBalance { get; set; }
        public int guestSaveBalance { get; set; }
        public int guestLoanBalance { get; set; }
        public string guestAccStatus { get; set; }

        public string guestPassword { get; set; }

        #region Guests: Check Balance
        //public void CheckGuestBalance(Guests g_balance)
        //{
        //    SqlConnection con = new SqlConnection("server=ARMSTRONG\\JACOBINSTANCE;database=bankingAdminDB;integrated security=true");
        //    SqlCommand cmdGetBalance = new SqlCommand("select * from customerAccounts where customerName = customerName", con);
        //    cmdGetBalance.Parameters.AddWithValue("@customerName", g_balance.guestName);
        //    con.Open();

        //    SqlDataReader readGetBalance = cmdGetBalance.ExecuteReader();

        //    if (readGetBalance.Read())
        //    {
        //        Console.WriteLine("ACCOUNT NUMBER: # " + readGetBalance[0]);
        //        Console.WriteLine(" ------------------------------------------- ");
        //        Console.WriteLine("ACCOUNT HOLDER: " + readGetBalance[1]);
        //        Console.WriteLine(" ------------------------------------------- ");
        //        Console.WriteLine("AVAILABLE CHECKING BALANCE: $ " + readGetBalance[3]);
        //        Console.WriteLine(" ------------------------------------------- ");
        //        Console.WriteLine("AVAILABLE SAVINGS BALANCE: $ " + readGetBalance[4]);
        //        Console.WriteLine(" ------------------------------------------- ");
        //        Console.WriteLine("CURRENT LOANS BALANCE: $ " + readGetBalance[5]);
        //        Console.WriteLine(" ------------------------------------------- ");
        //        Console.WriteLine("ACCOUNT STATUS: " + readGetBalance[6]);

                

        //    }
        //    con.Close();

        //}
        public List<Guests> CheckGuestBalances()
        {
            SqlConnection con = new SqlConnection("server=ARMSTRONG\\JACOBINSTANCE;database=bankingAdminDB;integrated security=true");
            SqlCommand cmdCheckBalance = new SqlCommand("select customerNo, customerName, customerAccCheckBalance, customerAccSaveBalance, customerAccLoanBalance from customerAccounts", con);
            con.Open();
            SqlDataReader readGuestBalance = cmdCheckBalance.ExecuteReader();
            List<Guests> seeGuestBalance = new List<Guests>();
            while (readGuestBalance.Read())
            {
                seeGuestBalance.Add(new Guests()
                {
                    guestNo = (int)readGuestBalance[0],
                    guestName = readGuestBalance[1].ToString(),
                    guestCheckBalance = (int)readGuestBalance[2],
                    guestSaveBalance = (int)readGuestBalance[3],
                    guestLoanBalance = (int)readGuestBalance[4]
                });
            }
            readGuestBalance.Close();
            con.Close();
            return seeGuestBalance;
        }
        #endregion

        #region Guests: Check Account Status
        public bool CheckGuestAccountsExist(int p_accNo)
        {
            SqlConnection con = new SqlConnection("server=ARMSTRONG\\JACOBINSTANCE;database=bankingAdminDB;integrated security=true");
            SqlCommand cmdCheckAccounts = new SqlCommand("select * from customerAccounts where customerNo = @customerNo", con);
            cmdCheckAccounts.Parameters.AddWithValue("@customerNo", p_accNo);

            con.Open();
            int count = (int)cmdCheckAccounts.ExecuteNonQuery();
            con.Close();
            if (count == 1)
            {
                return false; 
            }
            return true;
        }
        #endregion

        #region Guests: Update Password
        public string UpdateGuestPass(Guests p_accObj)
        {
            SqlConnection con = new SqlConnection("server=ARMSTRONG\\JACOBINSTANCE;database=bankingAdminDB;integrated security=true");
            SqlCommand cmdUpdateGuestPass = new SqlCommand("update customerLogin set customerPassword = customerPassword where customerUsername = customerUsername", con);
            cmdUpdateGuestPass.Parameters.AddWithValue("@customerUsername", p_accObj.guestName);
            cmdUpdateGuestPass.Parameters.AddWithValue("@customerPassword", p_accObj.guestName);
            con.Open();
            int UpdateResult = cmdUpdateGuestPass.ExecuteNonQuery();
            con.Close();
            if (UpdateResult > 0)
            {
                return "Account Information Updated Successfully.";
            }
            return "Unable to find account.";


        }
        #endregion
    }
}
