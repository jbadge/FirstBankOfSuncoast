using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
// using CsvHelper.Configuration;

namespace FirstBankOfSuncoast
{

    // TransactionDatabase Object   
    // --------------------------------------
    // |                                    |
    // |   List<Transaction>                |
    // |   Transactions                     |
    // |                                    |
    // -------------------------------------

    class TransactionDatabase
    {
        // var list = new List<Transaction>();
        // CURRENTLY NOT NEEDED CODE, BUT WOULD LIKE TO IMPLEMENT FOLLOWING LINE
        public string fileName = "statement.csv";

        private List<Transaction> listOfTransactions { get; set; } = new List<Transaction>();

        public void LoadTransactions()
        {
            // var file = PromptForString("What is the name of the file you would like to load?");
            if (File.Exists("statement.csv"))
            {
                // Create a file reader to read from statement.csv
                var fileReader = new StreamReader("statement.csv");

                // Create a configuration that indicates this CSV file has no header
                var csvReader = new CsvReader(fileReader, CultureInfo.InvariantCulture);

                // Replace our BLANK list of transactions with the ones in the CSV file!
                listOfTransactions = csvReader.GetRecords<Transaction>().ToList();

                fileReader.Close();
            }
        }

        public void SaveTransactions()
        {
            var fileName = "statements.csv";
            var fileWriter = new StreamWriter(fileName);
            var csvWriter = new CsvWriter(fileWriter, CultureInfo.InvariantCulture);
            csvWriter.WriteRecords(listOfTransactions);
            fileWriter.Close();
        }

        public void GetBalance(string entry)
        {
            if (entry == "S")
            {
                var list = listOfTransactions.Sum(x => x.Savings);
                Console.WriteLine($"{list}");
                DialogueRefresher();
            }
            else if (entry == "C")
            {
                var list = listOfTransactions.Sum(x => x.Checking);
                Console.WriteLine($"{list}");
                DialogueRefresher();
            }
        }

        public List<Transaction> GetAllTransactions()
        {
            return listOfTransactions;
        }

        public void PrintTransactions()
        {
            foreach (var item in listOfTransactions)
            {
                Console.WriteLine($"{item} is the item");
                DialogueRefresher();
            }
        }

        public Transaction FindOneTransaction(int transactionToFind)
        {
            return listOfTransactions.First(x => x.Savings == transactionToFind);
        }

        // Create ADD Transaction (This was AddTransaction)
        public void Deposit(Transaction transactionToDeposit)
        {
            listOfTransactions.Add(transactionToDeposit);
            SaveTransactions();
        }

        // Create DELETE Transaction (This was DeleteTransaction)
        public void Withdraw(Transaction transactionToWithdraw)
        {
            listOfTransactions.Remove(transactionToWithdraw);
            SaveTransactions();
        }

        public void DialogueRefresher()
        {
            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey(true).Key.ToString();
            Console.Clear();
        }

        static void Debug(int num)
        {
            Console.WriteLine($"Debug {num}");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey(true).Key.ToString();
        }

    }
}

