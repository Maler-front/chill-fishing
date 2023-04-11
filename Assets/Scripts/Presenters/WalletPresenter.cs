using System;

public class WalletPresenter : IPresenter
{
    private WalletModel _walletModel;
    private WalletView _walletView;

    public WalletPresenter(WalletView walletView)
    {
        _walletView = walletView;
        _walletModel = WalletModel.Instance;
        Activate();
    }

    public void Activate()
    {
        WalletModel.Instance.OnCoinsChanged += WalletModel_OnCoinsChanged;
    }

    public void Deactivate()
    {
        WalletModel.Instance.OnCoinsChanged -= WalletModel_OnCoinsChanged;
    }

    private void WalletModel_OnCoinsChanged(object sender, EventArgs e)
    {
        _walletView.ChangeCoinsTo(WalletModel.Instance.coins);
    }
}
