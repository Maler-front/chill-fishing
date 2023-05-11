using System;
using UnityEngine;

public class WalletModel
{
    public static WalletModel Instance { get; private set; }

    public int coins { get; private set; }

    public Action<int> OnCoinsChanged;

    public WalletModel()
    {
        if (Instance == null)
            Instance = this;
    }

    public WalletModel(int startCoins)
    {
        coins = startCoins;
        if (Instance == null)
            Instance = this;
    }

    public void AddCoins(int value)
    {
        coins += value;
        OnCoinsChanged?.Invoke(coins);
    }

    public bool TrySpendCoins(int value)
    {
        if(value > coins)
            return false;

        coins -= value;
        OnCoinsChanged?.Invoke(coins);
        return true;
    }
}
