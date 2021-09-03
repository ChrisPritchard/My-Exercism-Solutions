using System;

static class SavingsAccount
{
    public static float InterestRate(decimal balance)
    {
        if (balance < 0) return -3.213f;
        else if (balance < 1000) return 0.5f;
        else if (balance < 5000) return 1.621f;
        else return 2.475f;
    }

    public static decimal AnnualBalanceUpdate(decimal balance) => 
        balance + balance * (decimal)InterestRate(balance)/(balance < 0 ? -100m : 100m);

    public static int YearsBeforeDesiredBalance(decimal balance, decimal targetBalance)
    {
        var years = 0;
        do
        {
            balance = AnnualBalanceUpdate(balance);
            years ++;
        }
        while (balance < targetBalance);
        return years;
    }
}
