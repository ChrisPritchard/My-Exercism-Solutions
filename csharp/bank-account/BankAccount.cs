using System;

public class BankAccount
{
    private bool isOpen = false;
    private float balance;

    private object lockObject = new object();

    public void Open() => isOpen = true;

    public void Close() => isOpen = false;

    public float Balance 
    { 
        get 
        { 
            if(!isOpen)
                throw new InvalidOperationException("This account is closed");
            return balance;
        }
        private set { balance = value; }
    }

    public void UpdateBalance(float change)
    {
        lock (lockObject)
        {
            Balance += change;
        }
    }
}
