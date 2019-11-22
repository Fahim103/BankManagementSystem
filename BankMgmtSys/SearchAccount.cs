using System;
using System.IO;

namespace BankMgmtSys
{
    public class SearchAccount
    {
        /**
         * Returns Account Number
         */
        private static int GetAccountNumber()
        {
            bool validAccountNumber = false;
            int accountNumber = -1;
            do
            {
                Console.Write("Enter Account Number: ");
                string number = Console.ReadLine();
                int.TryParse(number, out accountNumber);
                if (accountNumber != 0)
                {
                    validAccountNumber = true;
                }
                else
                {
                    bool answer = Utility.CheckAccountNumberLength(number);
                    // If user pressed 'n' when asked if they wanna search for another account, then answer will be false
                    // So we invert it to make it true and exit the loop
                    if (!answer)
                    {
                        validAccountNumber = true;
                    }
                }

            } while (validAccountNumber != true);
            return accountNumber;
        }

        /// <summary>
        /// A Method that takes account number as input and returns the path to that account if found
        /// </summary>
        /// <returns>File Path</returns>
        public static string SearchAccountInfo()
        {
            int accountNumber = GetAccountNumber();
            if (accountNumber == 0)
            {
                return null;
            }
            string filePath = "";
            filePath = FindAccountFile(accountNumber);

            return filePath;
        }

        private static string FindAccountFile(int accountNumber)
        {
            string fileName = accountNumber.ToString();

            // MUST BE Changed based on users PC DIRECTORY
            string folderPath = @"";
            string folderName = @"\accounts";
            DirectoryInfo accountsDirectory = new DirectoryInfo(folderPath + folderName);
            FileInfo[] filesInDir = accountsDirectory.GetFiles("*" + fileName + "*.*");

            if (filesInDir.Length > 0)
            {
                Console.WriteLine("Account Found!");
                foreach (FileInfo foundFile in filesInDir)
                {
                    string fullName = foundFile.FullName;
                    return fullName;
                }
            }
            else
            {
                Console.WriteLine("Account Not Found");
                Console.Write("Check another account (y/n)?");
                string answer = Console.ReadLine();
                if (answer.ToLower().Equals("y"))
                {
                    // Check for another account
                    string path = SearchAccountInfo();
                    if (path != null)
                    {
                        return path;
                    }
                }
                else
                {
                    // Go to Main Menu
                    Console.Clear();
                    MainMenu.ShowMenu();
                }
            }
            return null;
        }

        /// <summary>
        /// A method that is used to search for an account and if available show its info
        /// </summary>
        public static void DisplayAccountDetails(string optionalPath = null)
        {
            string fileName = "";
            if (optionalPath == null)
            {
                fileName = SearchAccountInfo();
                if (fileName == null)
                {
                    MainMenu.ShowMenu();
                }
            }
            else
            {
                fileName = optionalPath;
            }
            try
            {
                // Write file contents on console.     
                using (StreamReader sr = File.OpenText(fileName))
                {
                    Console.Clear();
                    Console.WriteLine("Account Details: ");
                    string s = "";
                    while ((s = sr.ReadLine()) != null)
                    {
                        if (s.Contains("Transaction:"))
                            break;
                        Console.WriteLine(s);
                    }
                }
                Console.WriteLine("");
                if (optionalPath != null)
                {
                    return;
                }
                Console.WriteLine("Check another account (y/n)?");
                string input = Console.ReadLine();
                if (input.ToLower().Equals("y"))
                {
                    DisplayAccountDetails();
                }
                else
                {
                    Console.Clear();
                    MainMenu.ShowMenu();
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.ToString());
            }
        }
    }
}
