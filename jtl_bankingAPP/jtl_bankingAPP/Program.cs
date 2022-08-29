using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using jtl_bankingLIB;

namespace jtl_bankingAPP
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool startLogin = true;
            while (startLogin == true)
            {

                Console.WriteLine("Welcome to Sixth Fourth Bank");
                Console.WriteLine("");
                Console.WriteLine("Please choose one of the following options: ");
                Console.WriteLine("Select 1: Admin Login");
                Console.WriteLine("Select 2: Guest Login");

                int choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    #region Admin: Login & Menu
                    case 1:
                        Console.WriteLine("Enter your Admin Username: ");
                        string admin_name = Console.ReadLine();
                        Console.WriteLine("Enter your Admin Password: ");
                        string admin_pass = Console.ReadLine();

                        Security secAdminObj = new Security();
                        bool adminloginResult = secAdminObj.CheckLoginCredentials(admin_name, admin_pass, LoginType.Admin);
                        if (adminloginResult == true)
                        {
                            Console.WriteLine("Welcome to the Administrator Options Screen. Select one of the menu options to get started!");
                            Console.WriteLine("Press 1: to OPEN a New Account");
                            Console.WriteLine("Press 2: to RETRIEVE ALL Accounts");
                            Console.WriteLine("Press 3: to initiate a WITHDRAWAL");
                            Console.WriteLine("Press 4: to initiate a DEPOSIT");
                            Console.WriteLine("Press 5: to initiate a TRANSFER");
                            Console.WriteLine("Press 6: to DELETE an Account");
                            Console.WriteLine("Press 7: to ACTIVATE or Re-ACTIVATE an Account");
                            Console.WriteLine("Press 8: to EXIT");
                            Admins accObj = new Admins(); //CALL METHODS from LIB
                            Admins newAcc = new Admins(); //SEARCH, ADD, UPDATE accounts in records
                            Transactions wtransObj = new Transactions(); //Transaction methods
                            bool check = false;
                            int adminChoice = Convert.ToInt32(Console.ReadLine());
                            switch (adminChoice)
                            {
                                #region Admin: Open New Account
                                case 1: 
                                    Console.WriteLine("To OPEN a new account for a client, please assign an account number");
                                    newAcc.customerNo = Convert.ToInt32(Console.ReadLine());

                                    Console.WriteLine("Please assign a name (minimum of 3 characters) for this account: ");
                                    newAcc.customerName = Console.ReadLine();
                                    Console.WriteLine("Provide designate a type for this account: Guest or Admin?");
                                    newAcc.customerAccType = Console.ReadLine();
                                    Console.WriteLine("Enter the initial deposit to open the account.");
                                    newAcc.customerAccCheckBalance = Convert.ToInt32(Console.ReadLine());
                                    Console.WriteLine("Assign an account status: Active, Locked, or Frozen.");
                                    newAcc.customerAccStatus = Console.ReadLine();

                                    string result = accObj.OpenAccount(newAcc);
                                    Console.WriteLine(result);

                                    break;
                                #endregion

                                #region Admin: Retrieve All Accounts
                                case 2: 
                                    List<Admins> accList = accObj.GetAllAccounts();
                                    int totalAcc = 0;
                                    Console.WriteLine("Customer Number \tCustomer Name \tCustomer Account Type \tCustomer Account Balance \tCustomer Account Status \t");
                                    foreach (var item in accList)
                                    {
                                        Console.WriteLine(item.customerNo + "\t" + item.customerName + "\t" + item.customerAccType + "\t" + item.customerAccCheckBalance + "\t" + item.customerAccStatus);
                                        Console.WriteLine("Customer Account Number: " + item.customerNo);
                                        Console.WriteLine("Customer Name: " + item.customerName);
                                        Console.WriteLine("Customer Account Type: " + item.customerAccType);
                                        Console.WriteLine("Customer Account Balance: " + item.customerAccCheckBalance);
                                        Console.WriteLine("Customer Account Status: " + item.customerAccStatus);
                                        Console.WriteLine(" --------------------------------------------------------------- ");

                                        Console.WriteLine();

                                        totalAcc = totalAcc + 1;

                                    }
                                    Console.WriteLine("Total Customers : " + totalAcc);
                                    return;
                                #endregion

                                #region Admin: Initiate Withdrawal
                                case 3:
                                    Console.WriteLine("To initiate a WITHDRAWAL, first enter the Customer Number.");
                                    newAcc.customerNo = Convert.ToInt32(Console.ReadLine());
                                    check = accObj.CheckAccountsExist(newAcc.customerNo);
                                    if (!check)
                                    {
                                        Console.WriteLine("Enter the WITHDRAWAL AMOUNT in $: ");
                                        double w_Amount = Convert.ToDouble(Console.ReadLine());
                                        var w_Obj = new Admins();
                                        w_Obj.Withdraw(w_Amount);
                                    }
                                    else
                                    {
                                        Console.WriteLine("Unable to find customer account number. Please try again");
                                    }
                                    return;
                                #endregion

                                #region Admin: Initiate Deposit
                                case 4:
                                    Console.WriteLine("To initiate a DEPOSIT, first enter the Customer Number.");
                                    newAcc.customerNo = Convert.ToInt32(Console.ReadLine());
                                    check = accObj.CheckAccountsExist(newAcc.customerNo);
                                    if (!check)
                                    {
                                        Console.WriteLine("Enter the DEPOSIT AMOUNT in $: ");
                                        double d_Amount = Convert.ToDouble(Console.ReadLine());
                                        var d_Obj = new Admins();
                                        d_Obj.Deposit(d_Amount);
                                    }
                                    else
                                    {
                                        Console.WriteLine("Unable to find customer account number. Please try again");
                                    }
                                    return;
                                #endregion

                                #region Admin: Initiate Transfer
                                case 5:
                                    Console.WriteLine("To initiate a WIRE TRANSFER: First, enter the Account Number to transfer FROM: ");
                                    int t_fromAccount = Convert.ToInt32(Console.ReadLine());
                                    Console.WriteLine("Enter the Account Number to transfer TO: ");
                                    int t_toAccount = Convert.ToInt32(Console.ReadLine());
                                    Console.WriteLine("Enter the amount you wish to transfer in $: ");
                                    int t_Amount = Convert.ToInt32(Console.ReadLine());
                                    Console.WriteLine("Press ENTER to continue...");
                                    string t_transBy = Console.ReadLine();


                                    string wtransResults = wtransObj.BalanceTransfer(t_fromAccount, t_toAccount, t_Amount, t_transBy, LoginType.Admin);
                                    Console.WriteLine(wtransResults);

                                    return;
                                #endregion

                                #region Admin: Delete Account
                                case 6:
                                    Console.WriteLine("To DELETE an account, enter the Account Number to be deleted. CAUTION: Deleting an account is irreversible.");
                                    int delAccNo = Convert.ToInt32(Console.ReadLine());
                                    string delResult = accObj.DeleteAccounts(delAccNo);
                                    Console.WriteLine(delResult);

                                    return;
                                #endregion

                                #region Admin: Activate Accounts
                                case 7:
                                    Console.WriteLine("To ACTIVATE or RE-ACTIVATE an account: First, enter the Account Number you wish to ACTIVATE or RE-ACTIVATE. ");
                                    newAcc.customerAccStatus = Console.ReadLine();
                                    string actAccResult = accObj.ActivateAccounts(newAcc);
                                    Console.WriteLine(actAccResult);

                                    return;
                                #endregion

                                #region Admin: Exit Menu
                                case 8:
                                    bool continueMenu = true;
                                    while (continueMenu == true)
                                    {
                                        Console.WriteLine("Are you sure you want to exit this screen?");
                                        Console.WriteLine("1. Press 1 for YES");
                                        Console.WriteLine("2. Press 2 for NO");
                                        string menuOption = Console.ReadLine();

                                        if (menuOption == "1")
                                        {
                                            Console.WriteLine("You are now leaving the screen.");
                                            Console.WriteLine("Thank you!");
                                            return;
                                        }
                                        if (menuOption == "2")
                                        {
                                            Console.WriteLine("Returning to main menu shortly. Press Any Key to Continue");
                                            return;
                                        }
                                    }
                                    return;
                                    #endregion
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid Adminstrator Credentials. Please try again");
                        }
                        return;
                    #endregion

                    #region Guest: Login & Menu
                    case 2:
                        Console.WriteLine("Enter your Guest Username: ");
                        string guest_name = Console.ReadLine();
                        Console.WriteLine("Enter your Guest Password: ");
                        string guest_pass = Console.ReadLine();

                        Security secGuestObj = new Security();
                        bool guestloginResult = secGuestObj.CheckLoginCredentials(guest_name, guest_pass, LoginType.Guest);
                        bool guestcontinueApp = true;
                        if (guestloginResult == false)
                        {
                            guestcontinueApp = false;
                        }
                        else
                        {
                            Console.WriteLine("Invalid Login credentials. Please try again.");
                        }
                        while (guestcontinueApp == false)
                        {
                            Console.Clear();

                            Console.WriteLine("Welcome to the Guest Menu Screen. What would you like to do?");
                            Console.WriteLine("Press 1: CHECK your Balance");
                            Console.WriteLine("Press 2: Make a WITHDRAWAL");
                            Console.WriteLine("Press 3: Make a DEPOSIT");
                            Console.WriteLine("Press 4: Make a TRANSFER");
                            Console.WriteLine("Press 5: View Transaction History");
                            Console.WriteLine("Press 6: Change Password");
                            Console.WriteLine("Press 7: To EXIT");

                            int guestChoice = Convert.ToInt32(Console.ReadLine());
                            Guests accObj = new Guests();
                            Guests newAcc = new Guests();
                            bool check = false;
                            Transactions transObj = new Transactions();

                            
                            switch (guestChoice)
                            {
                                #region Guest: Check Balance
                                case 1:
                                    //Console.WriteLine("Please enter the Account Holder's Name: ");
                                    //newAcc.guestName = Console.ReadLine();

                                    //string guestBalanceResult = accObj.CheckGuestBalance();
                                    //return;

                                    List<Guests> guestBalance = accObj.CheckGuestBalances();
                                    Console.WriteLine("Customer Number \tCustomer Name \tCustomer Account Type \tCustomer Account Balance \tCustomer Account Status \t");
                                    foreach (var item in guestBalance)
                                    {
                                        Console.WriteLine(item.guestNo + "\t" + item.guestName + "\t" + item.guestCheckBalance + "\t" + item.guestSaveBalance + "\t" + item.guestLoanBalance);
                                        Console.WriteLine("Customer Account Number: " + item.guestNo);
                                        Console.WriteLine("Customer Name: " + item.guestName);
                                        Console.WriteLine("Customer Account Type: " + item.guestCheckBalance);
                                        Console.WriteLine("Customer Account Balance: " + item.guestSaveBalance);
                                        Console.WriteLine("Customer Account Status: " + item.guestLoanBalance);
                                        Console.WriteLine(" --------------------------------------------------------------- ");

                                        Console.WriteLine();

                                    }
                                    return;
                                #endregion

                                #region Guest: Make a Withdrawal
                                case 2:
                                    Console.WriteLine("To initiate a withdrawal, first enter the Customer Number.");
                                    newAcc.guestNo = Convert.ToInt32(Console.ReadLine());
                                    
                                    Console.WriteLine("Enter the withdrawal amount in $: ");
                                    double w_Amount = Convert.ToDouble(Console.ReadLine());
                                    var w_Obj = new Admins();
                                    w_Obj.Withdraw(w_Amount);
                                    
                                    return;
                                #endregion

                                #region Guest: Make a Deposit
                                case 3:
                                    Console.WriteLine("To initiate a Deposit, first enter the Customer Number.");
                                    newAcc.guestNo = Convert.ToInt32(Console.ReadLine());
                                    
                                    Console.WriteLine("Enter the deposit amount in $: ");
                                    double d_Amount = Convert.ToDouble(Console.ReadLine());
                                    var d_Obj = new Admins();
                                    d_Obj.Deposit(d_Amount);
                                    
                                    return;
                                #endregion

                                #region Guest: Make a Transfer
                                case 4:
                                    Console.WriteLine("To initiate a Wire Transer: First, enter the Account Number to transfer FROM: ");
                                    int t_fromAccount = Convert.ToInt32(Console.ReadLine());
                                    Console.WriteLine("Enter the Account Number to transfer TO: ");
                                    int t_toAccount = Convert.ToInt32(Console.ReadLine());
                                    Console.WriteLine("Enter the amount you wish to transfer in $: ");
                                    int t_Amount = Convert.ToInt32(Console.ReadLine());
                                    Console.WriteLine("Press ENTER to continue...");
                                    string t_transBy = Console.ReadLine();
                                    Transactions wtransObj = new Transactions();


                                    string wtransResults = wtransObj.BalanceTransfer(t_fromAccount, t_toAccount, t_Amount, t_transBy, LoginType.Admin);
                                    Console.WriteLine(wtransResults);

                                    return;
                                #endregion

                                #region Guest: Transaction History
                                case 5:
                                    List<Transactions> accList = transObj.TransactionHistory();
                                    Console.WriteLine("Customer Number \tCustomer Name \tCustomer Account Type \tCustomer Account Balance \tCustomer Account Status \t");
                                    foreach (var item in accList)
                                    {
                                        Console.WriteLine(item.customerNo + "\t" + item.customerName + "\t" + item.customerAccType + "\t" + item.customerAccCheckBalance + "\t" + item.customerAccStatus);
                                        Console.WriteLine("Customer Account Number: " + item.customerNo);
                                        Console.WriteLine("Customer Name: " + item.customerName);
                                        Console.WriteLine("Customer Account Type: " + item.customerAccType);
                                        Console.WriteLine("Customer Account Balance: " + item.customerAccCheckBalance);
                                        Console.WriteLine("Customer Account Status: " + item.customerAccStatus);
                                        Console.WriteLine(" --------------------------------------------------------------- ");

                                        Console.WriteLine();

                                    }
                                    return;
                                #endregion

                                #region Guest: Change Password
                                case 6:
                                    Console.WriteLine("Enter your NEW password.");
                                    newAcc.guestName = Console.ReadLine();
                                    //check = accObj.CheckGuestAccountsExist(newAcc.guestNo);
                                    //if (!check)
                                    //{
                                        //Console.WriteLine("Enter new password");
                                        //newAcc.guestPassword = Console.ReadLine();
                                        string updateGuestPass = accObj.UpdateGuestPass(newAcc);
                                        Console.WriteLine(updateGuestPass);

                                    //}
                                    //else
                                    //{
                                    //    Console.WriteLine("A guest with that username does not exist. Please try again.");
                                    //}
                                    break;
                                #endregion

                                #region Guest: Exit Menu
                                case 7:
                                    bool continueMenu = true;
                                    while (continueMenu == true)
                                    {
                                        Console.WriteLine("Are you sure you want to exit this screen?");
                                        Console.WriteLine("1. Press 1 for YES");
                                        Console.WriteLine("2. Press 2 for NO");
                                        string menuOption = Console.ReadLine();

                                        if (menuOption == "1")
                                        {
                                            Console.WriteLine("You are now leaving the screen.");
                                            Console.WriteLine("Thank you!");
                                            return;
                                        }
                                        if (menuOption == "2")
                                        {
                                            Console.WriteLine("Returning to main menu shortly. Press Any Key to Continue");
                                            return;
                                        }

                                    }
                                    return;
                                    #endregion

                            }

                        }
                        return;
                        #endregion

                }
                return;

            }

        }

    }

}




    
