using System;
using UnityEngine;

public class WalletModel
{
    public static WalletModel Instance { get; private set; }

    public int coins { get; private set; }

    public EventHandler OnCoinsChanged;

    public WalletModel()
    {
        if (Instance != null)
            Debug.LogError("Singleton error (in WalletModel class)!");

        Instance = this;
    }

    public void AddCoins(int value)
    {
        coins += value;
        OnCoinsChanged?.Invoke(this, EventArgs.Empty);
    }

    public bool TrySpendCoins(int value)
    {
        if(value > coins)
            return false;

        coins -= value;
        OnCoinsChanged?.Invoke(this, EventArgs.Empty);
        return true;
    }
}
