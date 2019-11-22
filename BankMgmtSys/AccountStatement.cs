using System;


namespace BankMgmtSys
{
    public class AccountStatement
    {
        public static void ShowAccountStatement()
        {
            string filePath = SearchAccount.SearchAccountInfo();
            if (filePath != null)
            {
                SearchAccount.DisplayAccountDetails(filePath);

                string accountInfo = Utility.GenerateEmailBodyWithAccountInfo(filePath);
                string transactionInfo = Utility.GetLatestFiveTransaction(filePath);

                // Split account info to find the email id to which mail is supposed to be sent
                string[] accountInfoArray = accountInfo.Split('\n');
                string[] emailLine = accountInfoArray[6].Split(' ');
                string sendTo = emailLine[1];

                string emailBody = accountInfo + transactionInfo;
                Console.WriteLine(transactionInfo);
                Console.WriteLine("Email Statement (y/n)?");
                string input = Console.ReadLine();
                if (input.ToLower().Equals("y"))
                {
                    Email.SendEmail(emailBody, sendTo);
                    Console.WriteLine("Email sent successfully!...");
                }
            }
            Console.WriteLine("Press any key to continue...");
            Console.Read();
            Console.Clear();
            MainMenu.ShowMenu();
        }
    }
}
