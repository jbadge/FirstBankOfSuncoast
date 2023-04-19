using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;

namespace FirstBankOfSuncoast
{
    class Program
    {
        // HOW DOES RIGHT CLICK "ADD CLASS" WORK in VSCODE?

        // Interface
        public static void WelcomeMessage()
        {
            Console.Clear();
            Console.WriteLine("--------------------");
            Console.WriteLine("Welcome to your bank");
            Console.WriteLine("--------------------");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey(true).Key.ToString();
            Console.Clear();
        }

        public static void Menu(TransactionDatabase database)
        {
            bool usingMenu = true;
            while (usingMenu)
            {
                var menuInput = PromptForChar("What would you like to do? \n(B)alance \n(T)ransactions \n(D)eposit \n(W)ithdrawal \n(Q) to quit.");

                switch (menuInput)
                {
                    case "Q":
                        usingMenu = false;
                        break;
                    case "B":
                        GetBalance(database);
                        break;
                    case "T":
                        ListTransactions(database);
                        break;
                    case "D":
                        MakeDeposit(database);
                        break;
                    case "W":
                        MakeWithdrawal(database);
                        break;
                    default:
                        Console.WriteLine("Please pick a valid option.");
                        database.DialogueRefresher();
                        break;
                }
            }
        }

        // Passthrough Functions
        public static void GetBalance(TransactionDatabase database)
        {
            database.GetBalance();
        }

        public static void ListTransactions(TransactionDatabase database)
        {
            database.ListTransactions();
        }

        public static void MakeDeposit(TransactionDatabase database)
        {
            database.MakeDeposit();
        }

        public static void MakeWithdrawal(TransactionDatabase database)
        {
            database.MakeWithdrawal();
        }

        // Helper Functions
        public static int PromptForInt(string prompt)
        {
            var inputWasInteger = false;
            int inputAsInteger = 0;

            while (!inputWasInteger)
            {
                var userInput = PromptForString(prompt);
                var isThisGoodInput = int.TryParse(userInput, out inputAsInteger);

                if (isThisGoodInput == true)
                {
                    inputWasInteger = true;
                }
                else
                {
                    Console.WriteLine("This is not a valid number. Please try again");
                    // DIALOGUE REFRESHER
                    Console.WriteLine();
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey(true).Key.ToString();
                    Console.Clear();
                }
            }
            return inputAsInteger;
        }

        public static string PromptForChar(string prompt)
        {
            Console.WriteLine(prompt);
            var userInput = Console.ReadKey(true).Key.ToString().ToUpper();
            Console.Clear();
            return userInput;
        }

        public static string PromptForString(string prompt)
        {
            Console.WriteLine(prompt);
            var userInput = Console.ReadLine();
            Console.Clear();
            return userInput;
        }

        // Main
        static void Main(string[] args)
        {
            // Could prompt for a filename here
            // string filename = "statement.csv";

            // MOVE THE NEW LIST CREATION SOMEWHERE ELSE
            var listOfTransactions = new List<Transaction>();
            var database = new TransactionDatabase();
            database.LoadTransactions();

            WelcomeMessage();

            Menu(database);
        }
    }
}