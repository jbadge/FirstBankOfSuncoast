namespace FirstBankOfSuncoast
{
    class Transaction
    {
        // All transaction values should be positive. Withdraw of $25 inputs and records positive 25
        // Cannot allow to go below $0
        public int Savings { get; set; }
        public int Checking { get; set; }

        // THESE ARE THE TRANSACTION ACTIONS
        public int Deposit { get; set; }
        public int Withdrawal { get; set; }

    }
}