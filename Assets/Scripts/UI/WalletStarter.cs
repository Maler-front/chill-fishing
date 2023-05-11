using UnityEngine;

public class WalletStarter : EntryLeaf
{
    [SerializeField]
    private GameObject _walletPrefab;

    protected override void AwakeComponent()
    {
        CreateWalletModel();

        base.AwakeComponent();
    }

    protected override void StartComponent()
    {
        CreateWalletVP();

        base.StartComponent();
    }

    public void CreateWalletModel()
    {
        if(WalletModel.Instance == null)
            new WalletModel();
    }

    public void CreateWalletVP()
    {
        GameObject wallet = Instantiate(_walletPrefab, transform.parent);
        WalletView walletView = wallet.GetComponent<WalletView>();
        new WalletPresenter(walletView);
        Destroy(gameObject);
    }
}
