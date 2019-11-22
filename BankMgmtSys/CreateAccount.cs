using System;
using System.IO;

namespace BankMgmtSys
{
    public class CreateAccount
    {
        public static void GetAccountInfoFromUser()
        {
            Console.Clear();
            string firstName, lastName, address, phone, email;
            Console.WriteLine("---------  Enter Account Details ---------------");
            Console.Write("First Name: ");
            firstName = Console.ReadLine();
            Console.Write("Last Name: ");
            lastName = Console.ReadLine();
            Console.Write("Address: ");
            address = Console.ReadLine();
            do
            {
                Console.Write("Phone: ");
                phone = Console.ReadLine();
            } while (Utility.CheckPhone(phone) != true);

            do
            {
                Console.Write("Email: ");
                email = Console.ReadLine();
            } while (Utility.CheckEmail(email) != true);
            Console.WriteLine("Is the information correct (y/n)?");
            string input = Console.ReadLine();
            if (input.ToLower().Equals("y"))
            {
                string accountNumber = AccountNumberGenerator.GetNewAccountNumber();
                CreateAccountInfoFile(accountNumber, firstName, lastName, address, phone, email);
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Enter information again (y/n)?");
                string enterInfoAgain = Console.ReadLine();
                if (enterInfoAgain.ToLower().Equals("y"))
                {
                    Console.Clear();
                    GetAccountInfoFromUser();
                }
                else
                {
                    Console.Clear();
                    MainMenu.ShowMenu();
                }

            }
        }

        /// <summary>
        /// The method is used to write account info inside a file which will be names as accountNo.txt
        /// The Account Number will be genereated // TODO
        /// </summary>
        /// <param name="accountNumber">accountNumber</param>
        /// <param name="firstName">firstName</param>
        /// <param name="lastName">lastName</param>
        /// <param name="address">address</param>
        /// <param name="phone">phone</param>
        /// <param name="email">email</param>
        static void CreateAccountInfoFile(string accountNumber, string firstName, string lastName, string address, string phone, string email)
        {
            string path = @"";
            string folderName = @"\accounts\";

            string uniqueAccountNumber = accountNumber + ".txt";
            string fileName = path + folderName + uniqueAccountNumber;

            try
            {
                // Create a new file     
                using (StreamWriter sw = File.CreateText(fileName))
                {
                    sw.WriteLine("Account No: {0}", accountNumber);
                    sw.WriteLine("Account Balance: {0}", 0);
                    sw.WriteLine("First Name: {0}", firstName);
                    sw.WriteLine("Last Name: {0}", lastName);
                    sw.WriteLine("Address: {0}", address);
                    sw.WriteLine("Phone: {0}", phone);
                    sw.WriteLine("Email: {0}", email);
                    sw.WriteLine("");
                    sw.WriteLine("Transaction:");
                }

                Console.WriteLine("Account created! details will be provided via email");
                Console.WriteLine("Account Number is : " + accountNumber);
                string emailBody = Utility.GenerateEmailBodyWithAccountInfo(fileName);
                Email.SendEmail(emailBody, email);
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.ToString());
            }
        }
    }
}
