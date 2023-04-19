using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;

namespace FirstBankOfSuncoast
{
    class TransactionDatabase
    {
        private List<Transaction> listOfTransactions { get; set; } = new List<Transaction>();

        // var file = PromptForString("What is the name of the file you would like to load?");
        // CURRENTLY NOT NEEDED CODE, BUT WOULD LIKE TO IMPLEMENT FOLLOWING LINE
        private string FileName = "statement.csv";

        public void LoadTransactions()
        {
            if (File.Exists(FileName))
            {
                // Create a file reader to read from statement.csv
                var fileReader = new StreamReader(FileName);
                // Create a configuration that indicates this CSV file has no header
                var csvReader = new CsvReader(fileReader, CultureInfo.InvariantCulture);
                // Replace our BLANK list of transactions with the ones in the CSV file!
                listOfTransactions = csvReader.GetRecords<Transaction>().ToList();
                fileReader.Close();
            }
            // ###########################################################
            // THIS IS CURRENTLY NEEDED TO WORK WHEN A FILE DOES NOT EXIST, 
            // BECAUSE OF THE LINES IN DEPOSIT/WITHDRAWAL THAT MANIPULATE
            // A LIST, SO WHEN ONE DOES NOT EXIST (WITH DATA),  IT FREAKS.
            // THIS GIVES US HEADERS AND A FILE, and DEFAULT VALUES
            // ###########################################################
            else
            {
                Transaction initialization = new Transaction();
                listOfTransactions.Add(initialization);
                SaveTransactions();
            }
        }

        public void SaveTransactions()
        {
            var fileWriter = new StreamWriter(FileName);
            var csvWriter = new CsvWriter(fileWriter, CultureInfo.InvariantCulture);
            csvWriter.WriteRecords(listOfTransactions);
            fileWriter.Close();
        }

        public void GetBalance()
        {
            var menuInput = Program.PromptForChar("What account would you like to see a balance for? \n(S)avings \n(C)hecking");
            switch (menuInput)
            {
                case "S":
                    var balance = listOfTransactions.Last(x => x.Savings > 0).Savings;
                    Console.WriteLine($"Your savings balance is ${balance}.");
                    DialogueRefresher();
                    break;
                case "C":
                    balance = listOfTransactions.Last(x => x.Checking > 0).Checking;
                    Console.WriteLine($"Your checking balance is ${balance}.");
                    DialogueRefresher();
                    break;
            }
        }

        public void ListTransactions()
        {
            var menuInput = Program.PromptForChar("What account would you like to see a list of transactions? \n(S)avings \n(C)hecking");
            switch (menuInput)
            {
                case "S":
                    Console.WriteLine("Here are your transactions for your Savings account:");
                    foreach (var item in listOfTransactions)
                    {
                        if (item.DepositToSavings > 0)
                        {
                            Console.WriteLine($" + ${item.DepositToSavings} Deposit");
                        }
                        else if (item.WithdrawalFromSavings > 0)
                        {
                            Console.WriteLine($" - ${item.WithdrawalFromSavings} Withdrawal");
                        }
                    }
                    Console.WriteLine($"You savings account balance is ${listOfTransactions.Last(x => x.Savings > 0).Savings}.");
                    DialogueRefresher();
                    break;

                case "C":
                    foreach (var item in listOfTransactions)
                    {
                        if (item.DepositToChecking > 0)
                        {
                            Console.WriteLine($" + ${item.DepositToChecking} Deposit");
                        }
                        else if (item.WithdrawalFromChecking > 0)
                        {
                            Console.WriteLine($" - ${item.WithdrawalFromChecking} Withdrawal");
                        }
                    }
                    Console.WriteLine($"You checking account balance is ${listOfTransactions.Last(x => x.Checking > 0).Checking}.");
                    DialogueRefresher();
                    break;
            }
        }

        public void MakeDeposit()
        {
            // CAN THESE 3 LINES (OR ANYTHING OTHER CODE BE REDUCED TO A FN BC THEY REPEAT SO MUCH?)
            Transaction transactionToDeposit = new Transaction();
            Transaction newTransaction = new Transaction();

            var menuInput = Program.PromptForChar("What account would you like to make a deposit into? \n(S)avings \n(C)hecking");

            switch (menuInput)
            {
                case "S":

                    // #################################################
                    // THIS BREAKS THINGS IF THE FILE DOES NOT EXIST!!!!


                    // NEEDS TO DO A CHECK/ INIT with ZERO
                    var savings = listOfTransactions.Last(x => x.Savings > 0).Savings;
                    // var savings = listOfTransactions.Last(x => x.Savings >= 0).Savings;
                    var number = CheckValueIsPos("deposit into your savings account?");

                    transactionToDeposit.DepositToSavings = number;
                    transactionToDeposit.Savings = savings + number;

                    SavingsHelper(transactionToDeposit);
                    break;

                case "C":

                    // NEEDS TO DO A CHECK/ INIT with ZERO
                    var checking = listOfTransactions.Last(x => x.Checking > 0).Checking;
                    // var checking = listOfTransactions.Last(x => x.Checking >= 0).Checking;
                    number = CheckValueIsPos("deposit into your checking account?");

                    transactionToDeposit.DepositToChecking = number;
                    transactionToDeposit.Checking = checking + number;

                    CheckingHelper(transactionToDeposit);
                    break;
            }
        }

        public void MakeWithdrawal()
        {
            Transaction transactionToWithdraw = new Transaction();
            Transaction newTransaction = new Transaction();
            var menuInput = Program.PromptForChar("What account would you like to make a withdrawal from? \n(S)avings \n(C)hecking");

            switch (menuInput)
            {
                case "S":

                    // NEEDS TO DO A CHECK/ INIT with ZERO
                    var savings = listOfTransactions.Last(x => x.Savings > 0);
                    // var savings = listOfTransactions.Last(x => x.Savings >= 0);
                    var number = CheckValueIsPos("withdraw from your savings account?");
                    bool fundsAvailable = false;

                    // CAN THIS LOOP BE PUT INTO HELPER FUNCTION? OR DOES SAVINGS/CHECKING DEPENDENCY PREVENT THAT?
                    while (!fundsAvailable)
                    {
                        if (savings.Savings - number >= 0)
                        {
                            transactionToWithdraw.WithdrawalFromSavings = number;
                            transactionToWithdraw.Savings = savings.Savings - number;
                            SavingsHelper(transactionToWithdraw);
                            fundsAvailable = true;
                        }
                        else
                        {
                            Console.WriteLine("You do not have enough funds available for this withdrawal. Please try again.");
                            // WHY WON'T DIALOGUE REFRESHER WORK HERE??? DialogueRefresher();
                            Console.WriteLine();
                            Console.WriteLine("Press any key to continue...");
                            Console.ReadKey(true).Key.ToString();
                            Console.Clear();
                            number = Program.PromptForInt("How much would you like to withdraw from your savings account?");// + endOfSentence);
                        }
                    }

                    break;

                case "C":

                    // NEEDS TO DO A CHECK/ INIT with ZERO
                    var checking = listOfTransactions.Last(x => x.Checking > 0);
                    // var checking = listOfTransactions.Last(x => x.Checking >= 0);
                    number = CheckValueIsPos("withdraw from your checking account?");
                    fundsAvailable = false;

                    while (!fundsAvailable)
                    {
                        if (checking.Checking - number >= 0)
                        {
                            transactionToWithdraw.WithdrawalFromChecking = number;
                            transactionToWithdraw.Checking = checking.Checking - number;
                            CheckingHelper(transactionToWithdraw);
                            fundsAvailable = true;
                        }
                        else
                        {
                            Console.WriteLine("You do not have enough funds available for this withdrawal. Please try again.");
                            // WHY WON'T DIALOGUE REFRESHER WORK HERE??? DialogueRefresher();
                            Console.WriteLine();
                            Console.WriteLine("Press any key to continue...");
                            Console.ReadKey(true).Key.ToString();
                            Console.Clear();
                            number = Program.PromptForInt("How much would you like to withdraw from your savings account?");// + endOfSentence);
                        }
                    }

                    break;
            }
        }

        public void SavingsHelper(Transaction transactionToTransfer)
        {
            listOfTransactions.Add(transactionToTransfer);
            Console.WriteLine($"Savings account balance is ${listOfTransactions.Last(x => x.Savings > 0).Savings}.");
            SaveTransactions();
            DialogueRefresher();
        }

        public void CheckingHelper(Transaction transactionToTransfer)
        {
            listOfTransactions.Add(transactionToTransfer);
            Console.WriteLine($"Checking account balance is ${listOfTransactions.Last(x => x.Checking > 0).Checking}.");
            SaveTransactions();
            DialogueRefresher();
        }

        public static int CheckValueIsPos(string endOfSentence)
        {
            var number = Program.PromptForInt("How much would you like to " + endOfSentence);
            bool positive = false;
            while (!positive)
            {
                if (number >= 0)
                {
                    positive = true;
                }
                else
                {
                    Console.WriteLine("This is not a valid number. Please try again");
                    // WHY WON'T DIALOGUE REFRESHER WORK HERE??? DialogueRefresher();
                    Console.WriteLine();
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey(true).Key.ToString();
                    Console.Clear();
                    number = Program.PromptForInt("How much would you like to " + endOfSentence);
                }
            }
            return number;
        }

        // public static int CheckFundsAvailable(Transaction transaction, int number)
        // {
        //     bool positive = false;
        //     while (!positive)
        //     {
        //         if (accountt >= 0)
        //         {
        //             positive = true;
        //         }
        //         else
        //         {
        //             Console.WriteLine("This is not a valid number. Please try again");
        //             // WHY WON'T DIALOGUE REFRESHER WORK HERE??? DialogueRefresher();
        //             Console.WriteLine();
        //             Console.WriteLine("Press any key to continue...");
        //             Console.ReadKey(true).Key.ToString();
        //             Console.Clear();
        //             number = Program.PromptForInt("How much would you like to " + endOfSentence);
        //         }
        //     }
        //     return number;
        // }

        public void DialogueRefresher()
        {
            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey(true).Key.ToString();
            Console.Clear();
        }

        static void Debug(int num, bool flag = false)
        {
            Console.WriteLine($"Debug {num}");
            if (flag == true)
            {
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey(true).Key.ToString();
            }
        }
    }
}

