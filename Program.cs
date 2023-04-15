using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using CsvHelper.Configuration;

namespace FirstBankOfSuncoast
{
    class Transaction
    {

        // All transaction values should be positive. Withdraw of $25 inputs and records positive 25
        // Cannot allow to go below $0
        public int checking { get; set; }
        public int savings { get; set; }

        // Ensure value is positive        
        public int deposits { get; set; }
        public int withdrawals { get; set; }

        // Something similar to use for this?
        // private List<Employee> Employees { get; set; } = new List<Employee>();
        // *******************************
        // GUESS SO! When to use it in Main and when to put it here, private?!?!?!?!?!
        private List<int> statements { get; set; } = new List<int>();

        // ### WHY STATIC? "SHARED PRIVATE INFO, ABOVE IS NOT, IS CHANGING ###
        static private string FileName = "statement.csv";

        public void LoadStatement()
        {
            if (File.Exists(FileName))
            {

                // Create a file reader to read from statement.csv
                var fileReader = new StreamReader(FileName);

                // Create a configuration that indicates this CSV file has no header
                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    // Tell the reader not to interpret the first
                    // row as a "header" since it is just the first record.
                    HasHeaderRecord = false,
                };

                var csvReader = new CsvReader(fileReader, config);

                // Creates our L ist<int< for from *READING* the data from the file!
                statements = csvReader.GetRecords<int>().ToList();

                fileReader.Close();
            }
        }
    }

    class Program
    {
        static void Menu()
        {
            // See list of transactions from Savings
            // See list of transactions from Checking

            // Make deposit transaction for savings
            // Write all transactions to file

            // Make deposit transaction for checking
            // Write all transactions to file

            // Make withdrawal transaction for savings
            // Write all transactions to file

            // Make withdrawal transaction for checking
            // Write all transactions to file

            // See the balance of savings and checking (together?)
            // See the balance of savings 
            // See the balance of checking

            Console.WriteLine("This is the menu");
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to your bank");

            // USER INPUT
            Console.Write("enter a number to store, or 'quit' to end: ");
            var input = Console.ReadLine().ToLower();
            var number = int.Parse(input);
            statements.Add(number);

            // WRITE
            var fileWriter = new StreamWriter(FileName);
            var csvWriter = new CsvWriter(fileWriter, CultureInfo.InvariantCulture);
            csvWriter.WriteRecords(statements);
            fileWriter.Close();

            // Load past transactions from a file
            // Menu
        }
    }
}