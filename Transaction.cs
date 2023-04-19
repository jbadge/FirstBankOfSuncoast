namespace FirstBankOfSuncoast
{
    class Transaction
    {
        // All transaction values should be positive. Withdraw of $25 inputs and records positive 25
        // Cannot allow to go below $0
        public int Savings { get; set; } = 0;
        public int Checking { get; set; } = 0;

        public int DepositToSavings { get; set; } = 0;
        public int WithdrawalFromSavings { get; set; } = 0;

        public int DepositToChecking { get; set; } = 0;
        public int WithdrawalFromChecking { get; set; } = 0;
    }
}