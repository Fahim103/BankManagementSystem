using System;

namespace BankMgmtSys
{
    public class MainMenu
    {
        /// <summary>
        /// The method shows menu to the user and asks what action they wanna take and redirects accordingly
        /// </summary>
        public static void ShowMenu()
        {
            Console.WriteLine("WELCOME TO SIMPLE BANKING SYSTEM");
            Console.WriteLine("");
            Console.WriteLine("1. Create a new account");
            Console.WriteLine("2. Search for an account");
            Console.WriteLine("3. Deposite");
            Console.WriteLine("4. Withdraw");
            Console.WriteLine("5. A/C statement");
            Console.WriteLine("6. Delete account");
            Console.WriteLine("7. Exit");
            Console.WriteLine("");
            bool validInput = false;
            int num;
            do
            {
                Console.Write("Enter your choice (1-7): ");
                string userInput = Console.ReadLine();

                int.TryParse(userInput, out num);
                if (num != 0 && num <= 7)
                {
                    validInput = true;
                }
                else
                {
                    Console.Clear();
                    ShowMenu();
                }
            } while (validInput != true);
            RedirectUser(num);
        }

        /// <summary>
        /// A switch case in this method will decide what to show to the user based on the input, 
        /// Respective methods will be called based on the input
        /// </summary>
        /// <param name="number">The Operation the user selects from the screen</param>
        private static void RedirectUser(int number)
        {
            switch (number)
            {
                case 1:
                    CreateAccount.GetAccountInfoFromUser();
                    break;
                case 2:
                    SearchAccount.DisplayAccountDetails();

                    break;
                case 3:
                    Deposit.DepositAmount();
                    break;
                case 4:
                    Withdraw.WithdrawAmount();
                    break;
                case 5:
                    AccountStatement.ShowAccountStatement();
                    break;
                case 6:
                    DeleteAccount.DeleteUserAccount();
                    break;
                case 7:
                    Console.WriteLine("Exiting console...");
                    Environment.Exit(0);
                    break;
                default:
                    break;
            }
        }
    }
}
