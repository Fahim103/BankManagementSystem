using System;
using System.IO;
using System.Linq;


namespace BankMgmtSys
{
    /// <summary>
    /// A utility class which has common functionality
    /// </summary>
    public class Utility
    {
        public static bool CheckPhone(string number)
        {
            // Must be less than or equal to 10 characters
            if (number.Length <= 10)
            {
                foreach (char c in number)
                {
                    if (c < '0' || c > '9')
                    {
                        Console.WriteLine("Phone Number can contain only digits");
                        return false;
                    }
                }
                return true;
            }
            else
            {
                Console.WriteLine("Phone Number Length cannot be more than 10");
                return false;
            }
        }

        public static bool CheckEmail(string email)
        {
            if (email.Contains('@'))
            {
                string[] val = email.Split('@');
                if (val.Length > 1 && val[1] != "")
                {
                    if (val[0].Equals(""))
                    {
                        Console.WriteLine("Email must contain something before @");
                        return false;
                    }
                    else if (val[1].Equals("gmail.com") || val[1].Equals("outlook.com"))
                    {
                        //Console.WriteLine("Valid Email");
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("Email must contain 'gmail.com' or 'outlook.com' at the end ");
                        return false;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid Email - Check if @ is there in email");
                    return false;

                }
            }
            else
            {
                Console.WriteLine("Invalid Email");
                return false;
            }
        }

        public static bool CheckAccountNumberLength(string number)
        {
            if (number.Length <= 10)
            {
                int.TryParse(number, out int accountNumber);
                if (accountNumber != 0)
                {
                    return true;
                }
                else
                {
                    Console.WriteLine("Account Number can contain only digits");
                    return AskUser();
                }
            }
            else
            {
                Console.WriteLine("Account Number Length cannot be more than 10");
                return AskUser();
            }
        }

        /// <summary>
        ///  Ask user if they want to do further operation or not
        /// </summary>
        /// <returns>True if y is pressed</returns>
        public static bool AskUser()
        {
            Console.Write("Check another account (y/n)?");
            string answer = Console.ReadLine();
            if (answer.ToLower().Equals("y"))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Receives the filename which will be read to send the email
        /// </summary>
        /// <param name="fileName"></param>
        public static string GenerateEmailBodyWithAccountInfo(string fileName)
        {
            string mailBody = "";
            using (StreamReader sr = File.OpenText(fileName))
            {
                string s = "";
                while ((s = sr.ReadLine()) != null)
                {
                    if (s.Contains("Transaction:"))
                    {
                        break;
                    }
                    mailBody += s;
                    mailBody += "\n";
                }
            }
            return mailBody;
        }

        public static string GetLatestFiveTransaction(string fileName)
        {
            string mailBody = "";
            using (StreamReader sr = File.OpenText(fileName))
            {
                string s = "";
                bool transactionFound = false;
                int count = 0;
                while ((s = sr.ReadLine()) != null)
                {
                    if (transactionFound != true)
                    {
                        if (!s.Contains("Transaction:"))
                        {
                            continue;
                        }
                        else
                        {
                            transactionFound = true;
                        }
                    }
                    count++;
                    mailBody += s;
                    mailBody += "\n";
                    if (count > 5)
                    {
                        break;
                    }
                }
                if (count < 2)
                {
                    mailBody += "No Transaction Happened So far";
                }
            }
            return mailBody;
        }
    }
}
