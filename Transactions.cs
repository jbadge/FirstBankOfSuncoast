namespace FirstBankOfSuncoast
{
    class Transaction
    {
        // All transaction values should be positive. Withdraw of $25 inputs and records positive 25
        // Cannot allow to go below $0
        public int Checking { get; set; }
        public int Savings { get; set; }

        // Ensure value is positive        
        public int Deposits { get; set; }
        public int Withdrawals { get; set; }

        // Something similar to use for this?
        // private List<Employee> Employees { get; set; } = new List<Employee>();
        // Employee is the same as Transaction here, so 
        // *******************************
        // GUESS SO! When to use it in Main and when to put it here, private?!?!?!?!?!
        // private List<int> statements { get; set; } = new List<int>();

        // ### WHY STATIC? "SHARED PRIVATE INFO, ABOVE IS NOT, IS CHANGING ###
        // static private string FileName = "statement.csv";

        // I WOULD PREFER TO PUT THESE IN CLASS
        // public static void LoadStatement()
        // public static void SaveStatement()
    }
}