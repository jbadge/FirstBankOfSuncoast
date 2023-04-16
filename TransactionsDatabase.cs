using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
// using CsvHelper.Configuration;

namespace FirstBankOfSuncoast
{
    class TransactionsDatabase
    {
        static void Debug(string item)
        {
            Console.WriteLine($"Debug {item}");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey(true).Key.ToString();
        }

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

        public static void Menu(string file, List<Transaction> list)
        {
            bool usingMenu = true;
            while (usingMenu)
            {
                var menuInput = PromptForChar("What would you like to do? \n(B)alance"); // \n(T)ransactions \n(D)eposit \n(W)ithdrawal \n(Q) to quit.");

                var n = list.Select(x => x.Savings);
                PrintListOfTransactions(n);

                // var newTransaction = new Transaction();
                // var listOfTransactions = new List<Transaction>();

                switch (menuInput)
                {
                    // BALANCE
                    // case "B":

                    //     menuInput = PromptForChar("What account would you like to see a balance for? \n(S)avings \n(C)hecking");
                    //     switch (menuInput)
                    //     {
                    //         // SAVINGS
                    //         case "S":
                    //             // See list of transactions from Savings
                    //             var n = listOfTransactions.Select(x => x.Savings);
                    //             PrintListOfTransactions(n);
                    //             DialogueRefresher();
                    //             // ListTransactions(list.savings);
                    //             break;

                    //         // CHECKING
                    //         case "C":
                    //             // See list of transactions from Checking
                    //             n = listOfTransactions.Select(x => x.Checking);
                    //             PrintListOfTransactions(n);
                    //             DialogueRefresher();

                    //             // ListTransactions(list.checking);
                    //             break;
                    //     }
                    //     break;

                    // use newTransaction 
                    // LIST OF TRANSACTIONS
                    case "T":
                        menuInput = PromptForChar("What would you like to see? \n(S)avings \n(C)hecking");
                        switch (menuInput)
                        {
                            // SAVINGS
                            case "S":
                                // See list of transactions from Savings
                                // var n = list.Select(x => x.Savings);
                                // PrintListOfTransactions(n);
                                // ListTransactions(list.savings);
                                break;

                            // CHECKING
                            case "C":
                                // See list of transactions from Checking
                                // ListTransactions(list.checking);
                                break;
                        }
                        break;

                    // // DEPOSIT
                    // case "D":
                    //     menuInput = PromptForChar("Where would you like to make a deposit? \n(S)avings \n(C)hecking");
                    //     switch (menuInput)
                    //     {
                    //         // SAVINGS
                    //         case "S":
                    //             // Make deposit transaction for Savings
                    //             var number = PromptForInt("How much would you like to deposit into your savings account?");
                    //             newTransaction.Savings = number;

                    //             // Write all transactions to file
                    //             SaveStatement(file, listOfTransactions);
                    //             // GAVIN USED AN ADD METHOD. 
                    //             break;

                    //         // CHECKING
                    //         case "C":
                    //             // Make deposit transaction for checking
                    //             number = PromptForInt("How much would you like to deposit into your checking account?");
                    //             newTransaction.Checking = number;

                    //             // Write all transactions to file
                    //             SaveStatement(file, listOfTransactions);
                    //             break;
                    //     }
                    //     break;

                    // // WITHDRAWAL
                    // case "W":
                    //     menuInput = PromptForChar("What account would you like to make a withdrawal from? \n(S)avings \n(C)hecking");
                    //     switch (menuInput)
                    //     {
                    //         // SAVINGS
                    //         case "S":
                    //             // Make withdrawal transaction for savings
                    //             var number = PromptForInt("How much would you like to withdraw from your savings account?");
                    //             // Write all transactions to file
                    //             break;

                    //         // CHECKING
                    //         case "C":
                    //             // Make withdrawal transaction for checking
                    //             number = PromptForInt("How much would you like to withdraw from your checking account?");
                    //             // Write all transactions to file                                
                    //             break;
                    //     }
                    //     break;

                    // QUIT
                    case "Q":
                        usingMenu = false;
                        break;
                    default:
                        Console.WriteLine("Please pick a valid option.");
                        DialogueRefresher();
                        break;

                        // See the balance of savings and checking (together?)
                        // See the balance of savings 
                        // See the balance of checking
                }
            }
        }

        public static List<Transaction> LoadStatement(string file, List<Transaction> list)
        {
            //var list = new List<int>;
            if (File.Exists(file))
            {

                // Create a file reader to read from statement.csv
                var fileReader = new StreamReader(file);

                // Create a configuration that indicates this CSV file has no header
                var csvReader = new CsvReader(fileReader, CultureInfo.InvariantCulture);

                // Creates our List<int> for from *READING* the data from the file!
                list = csvReader.GetRecords<Transaction>().ToList();//(numbers);

                fileReader.Close();
            }
            return list;
        }

        public static void SaveStatement(string file, List<Transaction> obj)
        {
            var fileWriter = new StreamWriter(file);
            var csvWriter = new CsvWriter(fileWriter, CultureInfo.InvariantCulture);
            csvWriter.WriteRecords(obj);
            fileWriter.Close();
        }

        static void PrintListOfTransactions(List<Transaction> list)
        {
            foreach (var item in list)
            {
                Console.WriteLine($"{item} is the object in this list");
                DialogueRefresher();
            }
        }

        // public void AddTransaction(Transaction newTransaction)
        // {
        //     // Transaction.Add(newTransaction);
        // }

        static int sumForBalance(List<Transaction> list, string option)
        {
            return list.Sum(x => x.Savings);
            // PrintListOfTransactions(list.Sum(x => x.Savings);
        }

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
                    DialogueRefresher();
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

        static void DialogueRefresher()
        {
            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey(true).Key.ToString();
            Console.Clear();
        }
    }
}

