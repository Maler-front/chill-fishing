using System;

public class WalletPresenter : IPresenter
{
    private WalletModel _walletModel;
    private WalletView _walletView;

    public WalletPresenter(WalletView walletView)
    {
        _walletView = walletView;
        _walletModel = WalletModel.Instance;
        _walletView.ChangeCoinsTo(WalletModel.Instance.coins);
        Activate();
    }

    public void Activate()
    {
        _walletModel.OnCoinsChanged += WalletModel_OnCoinsChanged;
    }

    public void Deactivate()
    {
        _walletModel.OnCoinsChanged -= WalletModel_OnCoinsChanged;
    }

    private void WalletModel_OnCoinsChanged(int coins)
    {
        _walletView.ChangeCoinsTo(coins);
    }
}
