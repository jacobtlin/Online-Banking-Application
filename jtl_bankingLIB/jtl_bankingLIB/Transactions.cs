using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace jtl_bankingLIB
{
    public class Transactions : Admins
    {
        #region Transactions: Balance Transfer
        public string BalanceTransfer(int t_fromAccount, int t_toAccount, int t_transAmount, string t_transBy, LoginType t_loginType)
        {
            SqlConnection con = new SqlConnection("server=ARMSTRONG\\JACOBINSTANCE;database=bankingAdminDB;integrated security=true");
            SqlCommand cmdFrom = new SqlCommand("update customerAccounts set customerAccCheckBalance = customerAccCheckBalance - @Amount where customerNo = @FromAccount", con);
            cmdFrom.Parameters.AddWithValue("@Amount", t_transAmount);
            cmdFrom.Parameters.AddWithValue("@FromAccount", t_fromAccount);

            SqlCommand cmdTo = new SqlCommand("update customerAccounts set customerAccCheckBalance = customerAccCheckBalance + @Amount where customerNo = @ToAccount", con);
            cmdTo.Parameters.AddWithValue("@Amount", t_transAmount);
            cmdTo.Parameters.AddWithValue("@ToAccount", t_toAccount);

            SqlCommand cmdTransaction = new SqlCommand("insert into TransactionInfo values(GETDATE(), @FromAccount, @ToAccount, @Amount, @TransferredBy)", con);
            cmdTransaction.Parameters.AddWithValue("@FromAccount", t_fromAccount);
            cmdTransaction.Parameters.AddWithValue("@ToAccount", t_toAccount);
            cmdTransaction.Parameters.AddWithValue("@Amount", t_transAmount);
            
            if (t_loginType == 0)
            {
                cmdTransaction.Parameters.AddWithValue("@TransferredBy", "Admin");
            }
            else
            {
                cmdTransaction.Parameters.AddWithValue("@TransferredBy", "Guest");
            }


            con.Open();
            cmdFrom.ExecuteNonQuery();
            cmdTo.ExecuteNonQuery();
            cmdTransaction.ExecuteScalar();
            con.Close();

            return "Wire Transfer Success!";
        }
        #endregion

        #region Transactions: Transaction History
        public List<Transactions> TransactionHistory()
        {
            SqlConnection con = new SqlConnection("server=ARMSTRONG\\JACOBINSTANCE;database=bankingAdminDB;integrated security=true");
            SqlCommand cmdTransactHist = new SqlCommand("select * from TransactionInfo values(GETDATE(),(@TimeOfTransfer, @FromAccount, @ToAccount, @Amount))", con);
            con.Open();
            SqlDataReader readTransactHist = cmdTransactHist.ExecuteReader();
            List<Transactions> transactions = new List<Transactions>();
            while (readTransactHist.Read())
            {
                transactions.Add(new Transactions()
                {
                    customerNo = (int)readTransactHist[0],
                    customerName = readTransactHist[1].ToString(),
                    customerAccType = readTransactHist[2].ToString(),
                    customerAccStatus = readTransactHist[3].ToString(),
                });
            }
            readTransactHist.Close();
            con.Close();
            return transactions;
        }
        #endregion
    }
}
