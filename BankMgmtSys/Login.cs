using System;
using System.IO;


namespace BankMgmtSys
{
    public class Login
    {
        public static void InitializeLogin()
        {
            string username, password;
            do
            {
                username = "";
                password = "";

                Console.WriteLine("WELCOME TO SIMPLE BANKING SYSTEM");
                Console.WriteLine("LOGIN TO START");

                Console.Write("Enter Username: ");
                username = Console.ReadLine();

                Console.Write("Enter Password: ");

                do
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                    {
                        password += key.KeyChar;
                        Console.Write("*");
                    }
                    else
                    {
                        if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                        {
                            password = password.Substring(0, (password.Length - 1));
                            Console.Write("\b \b");
                        }
                        else if (key.Key == ConsoleKey.Enter)
                        {
                            break;
                        }
                    }
                } while (true);
            } while (LoginVerify(username, password) != 1);
            Console.Read();
            Console.Clear();
            MainMenu.ShowMenu();
        }

        static int LoginVerify(string username, string password)
        {
            try
            {
                // Create an instance of StreamReader to read from a file.
                // The using statement also closes the StreamReader.
                string filepath = @"";
                string filename = @"\login.txt";
                using (StreamReader sr = new StreamReader(filepath + filename))
                {
                    string line;
                    // Read and display lines from the file until the end of 
                    // the file is reached.
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] user = new string[2];
                        user = line.Split(' ');
                        if (user[0] == username)
                        {
                            if (user[1] == password)
                            {
                                // Users Credentials is valid, so should be allowed to login
                                Console.WriteLine("\n\nValid Credentials!... Please Enter Key to continue");
                                username = "";
                                password = "";
                                return 1;
                            }
                            else
                            {
                                Console.Clear();
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Invalid Password\n");
                                Console.ForegroundColor = ConsoleColor.White;
                                username = "";
                                password = "";
                                return 0;
                            }
                        }
                    }
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("INVALID ID\n");
                    Console.ForegroundColor = ConsoleColor.White;

                    username = "";
                    password = "";
                    return 0;
                }
            }
            catch (Exception e)
            {
                // Let the user know what went wrong.
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
                return 0;
            }
        }
    }
}
