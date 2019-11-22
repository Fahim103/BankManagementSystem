using System;
using System.Collections;
using System.IO;

namespace BankMgmtSys
{
    public class Deposit
    {
        /// <summary>
        /// The Method used to deposit to an account
        /// </summary>
        public static void DepositAmount()
        {
            int depositAmount = 0;
            string filePath = SearchAccount.SearchAccountInfo();
            if (filePath != null)
            {
                depositAmount = GetDepositAmount();
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
                        fileContents.Add(s);
                    }
                }

                // Write to the file the updated info
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
                            currentBalance += depositAmount;
                            outputFile.WriteLine("Account Balance: " + currentBalance);

                        }
                        else if (line.Contains("Transaction:"))
                        {
                            outputFile.WriteLine(line);
                            outputFile.WriteLine("Deposit " + depositAmount + " " + DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss"));
                        }
                        else
                        {
                            outputFile.WriteLine(line);
                        }
                    }
                }
                Console.WriteLine("Amount Deposited");
                Console.WriteLine("Press Any Key To Continue...");
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
        private static int GetDepositAmount()
        {
            bool validAmount = false;
            int amount = -1;
            do
            {
                Console.Write("Enter deposit ammount: ");
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
