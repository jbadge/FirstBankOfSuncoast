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
                    Console.WriteLine($"Your savings balance is ${balance}");
                    DialogueRefresher();
                    break;
                case "C":
                    balance = listOfTransactions.Last(x => x.Checking > 0).Checking;
                    Console.WriteLine($"Your checking balance is ${balance}");
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
                    foreach (var item in listOfTransactions) { Console.WriteLine($"{item.Savings}"); }
                    DialogueRefresher();
                    break;
                case "C":
                    foreach (var item in listOfTransactions) { Console.WriteLine($"{item.Checking}"); }
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
                    var savings = listOfTransactions.Last(x => x.Savings > 0);
                    var number = CheckValueIsPos("deposit into your savings account?");

                    transactionToDeposit.Deposit = number;
                    transactionToDeposit.Savings = savings.Savings + number;

                    SavingsHelper(transactionToDeposit);
                    break;

                case "C":
                    var checking = listOfTransactions.Last(x => x.Checking > 0);
                    number = CheckValueIsPos("deposit into your checking account?");

                    transactionToDeposit.Deposit = number;
                    transactionToDeposit.Checking = checking.Checking + number;

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
                    var savings = listOfTransactions.Last(x => x.Savings > 0);
                    var number = CheckValueIsPos("withdraw from your savings account?");

                    transactionToWithdraw.Withdrawal = number;
                    transactionToWithdraw.Savings = savings.Savings - number;

                    SavingsHelper(transactionToWithdraw);
                    break;

                case "C":
                    var checking = listOfTransactions.Last(x => x.Checking > 0);
                    number = CheckValueIsPos("withdraw from your checking account?");

                    transactionToWithdraw.Withdrawal = number;
                    transactionToWithdraw.Checking = checking.Checking - number;

                    CheckingHelper(transactionToWithdraw);
                    break;
            }
        }

        public void SavingsHelper(Transaction transactionToTransfer)
        {
            listOfTransactions.Add(transactionToTransfer);
            Console.WriteLine($"Your savings balance is ${listOfTransactions.Last(x => x.Savings > 0).Savings}");
            SaveTransactions();
            DialogueRefresher();
        }

        public void CheckingHelper(Transaction transactionToTransfer)
        {
            listOfTransactions.Add(transactionToTransfer);
            Console.WriteLine($"Your checking balance is ${listOfTransactions.Last(x => x.Checking > 0).Checking}");
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

