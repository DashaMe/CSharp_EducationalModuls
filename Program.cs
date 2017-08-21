using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODULE_FIRST_ONE
{
    class Program
    {
        static void Main(string[] args)
        {
            //Програма - банковская система.

            //Базовые требования:
            //В программе существует 2 роли admin и user. Admin может создавать пользовательские счета(на каждый счет один пользователь).
            //Admin может:
            ////	 - просматривать список счетов с указанием имен пользователей, остатком денег на счету и статусом blocked если пользователь заблокирован.
            ////   - блокировать пользователя от входа в систему.
            ////   - разблокировать только блокированных пользователей.
            ////   - добавлять новый счет(нового пользователя).
            //	 - удалять счет(пользователя).
            //User может:
            //	 - пополнять свой карточный счет.
            //   - снимать деньги со счета.

            string[,] Accounts = new string[4, 5] { { "Account", "User name", "$", "Status", "Password" }, { "124345645", "Stalone", "1000", "Active", "56S" }, { "16745", "Amanda Gant", "275", "Active", "4756Q" }, { "14545", "Reno Blansh", "465", "Active", "59056" } };

            while (true)
            {
                string logInName = LoginName();
                string logInPassword = LoginPassword();

                if (logInName == "Admin" && logInPassword == "1234")
                {
                    Console.WriteLine("Greatings to you, Admin!\n");
                    AdminMenu(ref Accounts);
                }
                else
                {
                    bool authentication = FindUser(logInName, logInPassword, Accounts);
                    if (authentication == true)
                    {
                        Console.WriteLine("Greatings to you, " + logInName + "\n");
                        UserMenu(logInName, logInPassword, ref Accounts);
                    }
                    else
                    {
                        Console.WriteLine("Sorry, I do not know you\n");
                    }
                }
            }
        }
        public static string LoginName()
        {
            Console.WriteLine("Input your name");
            string inputName = Console.ReadLine();
            Console.WriteLine("");
            return inputName;
        }

        public static string LoginPassword()
        {
            Console.WriteLine("Input your password");
            string inputPassword = Console.ReadLine();
            Console.WriteLine("");
            return inputPassword;
        }

        public static void AdminMenu(ref string[,] arr)
        {
            bool cycle = true;
            while (cycle == true)
            {
                Console.WriteLine("\nPlease, select menu number\n");
                Console.WriteLine("1. See the list of accounts");
                Console.WriteLine("2. Block user");
                Console.WriteLine("3. Activate blocked user");
                Console.WriteLine("4. Create new account");
                Console.WriteLine("5. Remove account");
                Console.WriteLine("6. Switch user");
                Console.WriteLine("7. Exit\n");
                int choice = int.Parse(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        {
                            AccountsList(arr);
                            ReturnToMenu();
                            break;
                        }
                    case 2:
                        {
                            BlockUser(ref arr);
                            ReturnToMenu();
                            break;
                        }
                    case 3:
                        {
                            ActivateUser(ref arr);
                            ReturnToMenu();
                            break;
                        }
                    case 4:
                        {
                            arr = CreateAccount(arr);
                            ReturnToMenu();
                            break;
                        }
                    case 5:
                        {
                            if (arr.GetLength(0) == 1)
                            {
                                Console.WriteLine("\nNo users are available\n");
                                ReturnToMenu();
                            }
                            else
                            {
                                arr = RemoveUser(arr);
                                ReturnToMenu();
                            }
                            break;
                        }
                    case 6:
                        {
                            cycle = false;
                            Console.Clear();
                            break;
                        }
                    case 7:
                        {
                            Environment.Exit(0);
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("\nSelection was wrong. Please, try again.\n");
                            break;
                        }
                }
            }

        }

        public static void UserMenu(string name, string pass, ref string[,] arr)
        {
            bool cycle = true;
            while (cycle == true)
            {
                Console.WriteLine("\nPlease, select menu number\n");
                Console.WriteLine("1. Amount of money on the account");
                Console.WriteLine("2. Fund the account");
                Console.WriteLine("3. Withdraw my money");
                Console.WriteLine("4. Switch user");
                Console.WriteLine("5. Exit\n");
                int choice = int.Parse(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        {
                            string money = UsersMoney(name, pass, arr);
                            Console.WriteLine(money);
                            ReturnToMenu();
                            break;
                        }
                    case 2:
                        {
                            Console.WriteLine("Input amount of money you want to add");
                            int addMoney = int.Parse(Console.ReadLine());
                            AddMoney(name, pass, addMoney, ref arr);
                            ReturnToMenu();
                            break;
                        }
                    case 3:
                        {
                            Console.WriteLine("Input amount of money you want to take");
                            int takeMoney = int.Parse(Console.ReadLine());
                            TakeMoney(name, pass, takeMoney, ref arr);
                            ReturnToMenu();
                            break;
                        }
                    case 4:
                        {
                            cycle = false;
                            Console.Clear();
                            break;
                        }
                    case 5:
                        {
                            Environment.Exit(0);
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("\nSelection was wrong. Please, try again.\n");
                            break;
                        }
                }
            }

        }

        static void ReturnToMenu()
        {
            string back;
            do
            {
                Console.WriteLine("\nClick 0 to return to the main menu\n");
                back = Console.ReadLine();
            }
            while (back != "0");
            Console.Clear();
        }

        static void AccountsList(string[,] arr)
        {
            if (arr.GetLength(0) == 1)
            {
                Console.WriteLine("\nNo users are available\n");
            }
            else
            {
                Console.WriteLine("\nAccounts list:\n");
                for (int i = 0; i < arr.GetLength(0); i++)
                {
                    if (i != 0)
                        Console.Write(i);
                    else
                        Console.Write(" ");

                    for (int j = 0; j < arr.GetLength(1); j++)
                    {
                        Console.Write("{0,12}", arr[i, j].Length >= 10 ? " " + arr[i, j].Substring(0, 7) + "... " : " " + arr[i, j].Substring(0, arr[i, j].Length) + " ");
                    }
                    Console.WriteLine();
                }
            }

        }

        static string[,] CreateAccount(string[,] arr)
        {
            string[,] addAccount = new string[arr.GetLength(0) + 1, arr.GetLength(1)];
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    addAccount[i, j] = arr[i, j];
                }
            }
            for (int i = 0; i < arr.GetLength(1); i++)
            {
                if (i == 0)
                {
                    Console.WriteLine("\nInput new user's account number");
                    addAccount[arr.GetLength(0), i] = Console.ReadLine();
                }
                else if (i == 1)
                {
                    Console.WriteLine("\nInput new user's name");
                    addAccount[arr.GetLength(0), i] = Console.ReadLine();
                }
                else if (i == 2)
                {
                    Console.WriteLine("\nInput new user's amount of money");
                    addAccount[arr.GetLength(0), i] = Console.ReadLine();
                }
                else if (i == 3)
                {
                    bool stopper = false;
                    while (stopper == false)
                    {
                        Console.WriteLine("\nInput new user's status - Active/Blocked");
                        string temp = Console.ReadLine();
                        if (temp == "Active")
                        {
                            addAccount[arr.GetLength(0), i] = temp;
                            stopper = true;
                        }
                        else if (temp == "Blocked")
                        {
                            addAccount[arr.GetLength(0), i] = temp;
                            stopper = true;
                        }
                        else
                        {
                            Console.WriteLine("\nUser status is incorrect.");
                        }
                    }
                }
                else if (i == 4)
                {
                    Console.WriteLine("\nInput new user's password\n");
                    addAccount[arr.GetLength(0), i] = Console.ReadLine();
                }

            }
            Console.WriteLine("\nUser successfully added\n");

            return addAccount;
        }

        public static void BlockUser(ref string[,] arr)
        {
            if (arr.GetLength(0) == 1)
            {
                Console.WriteLine("\nNo users are available\n");
            }
            else
            {
                int blockUser = 0;
                Console.WriteLine("\nChoose a User you want to block. Please, specify a number");
                AccountsList(arr);
                Console.WriteLine("");
                blockUser = int.Parse(Console.ReadLine());
                if (arr[blockUser, 3] == "Blocked")
                {
                    Console.WriteLine("\nUser is already blocked");
                }
                else
                {
                    arr[blockUser, 3] = "Blocked";
                    Console.WriteLine("\nThe user successfully blocked");
                }
            }
        }

        public static void ActivateUser(ref string[,] arr)
        {
            if (arr.GetLength(0) == 1)
            {
                Console.WriteLine("\nNo users are available\n");
            }
            else
            {
                int activateUser = 0;
                Console.WriteLine("\nChoose a User you want to activate. Please, specify a number");
                AccountsList(arr);
                Console.WriteLine("");
                activateUser = int.Parse(Console.ReadLine());
                if (arr[activateUser, 3] == "Active")
                {
                    Console.WriteLine("\nUser is active already");
                }
                else
                {
                    arr[activateUser, 3] = "Active";
                    Console.WriteLine("\nThe user successfully activated");
                }
            }
        }

        public static string[,] RemoveUser(string[,] arr)
        {

            string[,] arrMinus = new string[arr.GetLength(0) - 1, arr.GetLength(1)];
            int removeUser = 0;
            Console.WriteLine("\nChoose a user you want to remove. Please, specify a number");
            AccountsList(arr);
            Console.WriteLine("");
            int userNum = int.Parse(Console.ReadLine());
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                if (i != userNum)
                {
                    for (int j = 0; j < arr.GetLength(1); j++)
                    {
                        {
                            arrMinus[removeUser, j] = arr[i, j];
                        }
                    }
                    removeUser += 1;
                }

            }
            Console.WriteLine("\nThe User successfully removed");

            return arrMinus;
        }

        public static bool FindUser(string name, string pass, string[,] arr)
        {
            bool authentication = false;
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                if (arr[i, 1] == name && arr[i, 4] == pass && arr[i,3] == "Active")
                {
                    authentication = true;
                }
            }
            return authentication;
        }

        public static string UsersMoney(string name, string pass, string[,] arr)
        {
            string money = null;
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                if (arr[i, 1] == name && arr[i, 4] == pass)
                {
                    money = arr[i, 2];
                }
            }
            return money;
        }

        public static void AddMoney(string name, string pass, int moneyAdd, ref string[,] arr)
        {
            int newMoney = 0;
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                if (arr[i, 1] == name && arr[i, 4] == pass)
                {
                   newMoney = (int.Parse(arr[i, 2]) + moneyAdd);
                    arr[i, 2] = Convert.ToString(newMoney);
                }
            }
            Console.WriteLine("Money successfully added to your account");
        }

        public static void TakeMoney(string name, string pass, int moneyTake, ref string[,] arr)
        {
            int newMoney = 0;
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                if (arr[i, 1] == name && arr[i, 4] == pass)
                {
                    newMoney = (int.Parse(arr[i, 2]) - moneyTake);
                    arr[i, 2] = Convert.ToString(newMoney);
                }
            }
            Console.WriteLine("Money successfully taken from your account");
        }
    }
}
