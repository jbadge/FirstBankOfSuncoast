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
            // Console.Clear();
        }

        public static void Menu(TransactionDatabase database)  // (string file, List<Transaction> list)
        {
            bool usingMenu = true;
            while (usingMenu)
            {
                var menuInput = PromptForChar("What would you like to do? \n(B)alance"); // \n(T)ransactions \n(D)eposit \n(W)ithdrawal \n(Q) to quit.");

                // var n = database.Sum(x => x.Savings);
                // PrintListOfTransactions(n);

                // var newTransaction = new Transaction();
                // var listOfTransactions = new List<Transaction>();

                switch (menuInput)
                {

                    case "Q":
                        usingMenu = false;
                        break;

                    case "B":

                        menuInput = PromptForChar("What account would you like to see a balance for? \n(S)avings \n(C)hecking");

                        switch (menuInput)
                        {
                            case "S":
                                database.GetBalance("S");
                                break;
                            case "C":
                                database.GetBalance("C");
                                break;
                        }
                        break;

                    // SEE A LIST OF TRANSACTIONS
                    // case "T":
                    //     CheckingOrSavings();
                    //     switch (menuInput)
                    //     {
                    //         case "S":
                    //             database.PrintTransactions();
                    //             break;
                    //         case "C":
                    //             database.PrintTransactions();
                    //             break;
                    //     }
                    //     break;

                    // MAKE A DEPOSIT
                    case "D":
                        menuInput = PromptForChar("What account would you like to see a balance for? \n(S)avings \n(C)hecking");
                        switch (menuInput)
                        {
                            // SAVINGS
                            case "S":
                                var number = PromptForInt("How much would you like to deposit into your savings account?");
                                var newTransaction = new Transaction();
                                newTransaction.Savings = number;
                                database.Deposit(newTransaction);
                                // GAVIN USED AN ADD METHOD. 
                                break;

                            // CHECKING
                            case "C":
                                number = PromptForInt("How much would you like to deposit into your checking account?");
                                newTransaction = new Transaction();
                                newTransaction.Checking = number;
                                database.Withdraw(newTransaction);
                                // SaveStatement(file, listOfTransactions);
                                break;
                        }
                        break;

                    // MAKE A WITHDRAWAL
                    case "W":
                        menuInput = PromptForChar("What account would you like to see a balance for? \n(S)avings \n(C)hecking");
                        switch (menuInput)
                        {
                            // SAVINGS
                            case "S":

                                var number = PromptForInt("How much would you like to withdraw from your savings account?");
                                // Write all transactions to file
                                break;

                            // CHECKING
                            case "C":
                                number = PromptForInt("How much would you like to withdraw from your checking account?");
                                // Write all transactions to file                                
                                break;
                        }
                        break;
                    default:
                        Console.WriteLine("Please pick a valid option.");
                        database.DialogueRefresher();
                        break;
                }
            }
        }

        // DIALOGUE REFRESHER
        static int PromptForInt(string prompt)
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

        static string PromptForChar(string prompt)
        {
            Console.WriteLine(prompt);
            var userInput = Console.ReadKey(true).Key.ToString().ToUpper();
            Console.Clear();
            return userInput;
        }

        static string PromptForString(string prompt)
        {
            Console.WriteLine(prompt);
            var userInput = Console.ReadLine();
            Console.Clear();
            return userInput;
        }

        static void Debug(int num)
        {
            Console.WriteLine($"Debug {num}");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey(true).Key.ToString();
        }

        static void Main(string[] args)
        {
            // Could prompt for a filename here
            // string filename = "statement.csv";

            // MOVE THE NEW LIST CREATION SOMEWHERE ELSE
            var listOfTransactions = new List<Transaction>();
            var database = new TransactionDatabase();
            WelcomeMessage();
            database.LoadTransactions();
            Menu(database);
        }
    }
}


// public static string CheckingOrSavings()
// {
//     var menuInput = PromptForChar("What account would you like to see a balance for? \n(S)avings \n(C)hecking");
//     return menuInput;
// }

// //  public static string CheckingOrSavings()
// //         {
// //             var menuInput = PromptForChar("What account would you like to see a balance for? \n(S)avings \n(C)hecking");
// //             switch (menuInput)
// //             {
// //                 case "S":
// //                     database.GetBalance("S");
// //                     break;
// //                 case "C":
// //                     database.GetBalance("C");
// //                     break;
// //             }
// //             return menuInput;
// //         }

// CheckingOrSavings();
//                 Console.WriteLine($"{menuInput}");
//                 switch (menuInput)
//                 {
//                     case "S":
//                         database.GetBalance("S");
//                         break;
//                     case "C":
//                         database.GetBalance("C");
//                         break;
//                 }
//                 break;