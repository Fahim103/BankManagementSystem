using System;
using System.Collections;
using System.IO;

namespace BankMgmtSys
{
    public class Withdraw
    {
        /// <summary>
        /// The Method used to deposit to an account
        /// </summary>
        public static void WithdrawAmount()
        {
            int withdrawAmount = 0;
            string filePath = SearchAccount.SearchAccountInfo();
            if (filePath != null)
            {
                withdrawAmount = GetWithdrawAmount();
            }
            else
            {
                Console.Clear();
                MainMenu.ShowMenu();
            }

            ArrayList fileContents = new ArrayList();
            try
            {

                using (StreamReader sr = File.OpenText(filePath))
                {
                    string s = "";
                    while ((s = sr.ReadLine()) != null)
                    {
                        //Console.WriteLine(s);
                        fileContents.Add(s);
                    }
                }

                //Console.WriteLine(fileContents[1]); // Contains Account Balance Line in the txt file

                // Split the account balance line to get the current balance
                string[] accountBalanceLine = fileContents[1].ToString().Split(' ');

                // Check if withdrawal amount is more than current balance
                int accBalance;
                bool validWithdrawAmount = false;
                string strBalance = accountBalanceLine[accountBalanceLine.Length - 1]; // Balance should be in last index
                int.TryParse(strBalance, out accBalance);
                if (accBalance - withdrawAmount < 0)
                {
                    // Not enough balance
                    do
                    {
                        Console.WriteLine("Insufficient balance");
                        Console.WriteLine("Current balance is: " + accBalance);
                        withdrawAmount = GetWithdrawAmount();
                        if (accBalance - withdrawAmount >= 0)
                        {
                            // Valid ammount
                            validWithdrawAmount = true;
                        }

                    } while (validWithdrawAmount != true);

                }

                // Write to the file the updated info
                // Clear the file
                File.WriteAllText(filePath, string.Empty);
                foreach (string line in fileContents)
                {
                    using (StreamWriter outputFile = new StreamWriter(filePath, true))
                    {
                        if (line.Contains("Account Balance: "))
                        {
                            string[] arr = line.Split(' ');
                            string balance = arr[arr.Length - 1];
                            int currentBalance = 0;
                            int.TryParse(balance, out currentBalance);
                            currentBalance -= withdrawAmount;

                            outputFile.WriteLine("Account Balance: " + currentBalance);

                        }
                        else if (line.Contains("Transaction:"))
                        {
                            outputFile.WriteLine(line);
                            outputFile.WriteLine("Withdraw " + withdrawAmount + " " + DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss"));
                        }
                        else
                        {
                            outputFile.WriteLine(line);
                        }
                    }
                }
                Console.WriteLine("Amount Withdrawn");
                Console.WriteLine("Press any key to continue...");
                Console.Read();
                Console.Clear();
                MainMenu.ShowMenu();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }

        /// <summary>
        /// A Method which will promt user to enter deposit amount
        /// </summary>
        /// <returns>Amount Entered</returns>
        private static int GetWithdrawAmount()
        {
            bool validAmount = false;
            int amount = -1;
            do
            {
                Console.Write("Enter withdraw ammount: ");
                string input = Console.ReadLine();
                int.TryParse(input, out amount);
                if (amount > 0)
                {
                    validAmount = true;
                }
            } while (validAmount != true);
            return amount;
        }
    }
}
