using System;
using System.IO;


namespace BankMgmtSys
{
    public class DeleteAccount
    {
        public static void DeleteUserAccount()
        {
            string filePath = SearchAccount.SearchAccountInfo();
            if (filePath != null)
            {
                SearchAccount.DisplayAccountDetails(filePath);
                Console.WriteLine("Delete (y/n)?");
                string input = Console.ReadLine();
                if (input.ToLower().Equals("y"))
                {
                    File.Delete(filePath);
                    Console.WriteLine("Account Deleted!...");
                    Console.WriteLine("Press Enter to continue...");
                    Console.ReadLine();
                }
            }
            Console.Clear();
            MainMenu.ShowMenu();
        }
    }
}
