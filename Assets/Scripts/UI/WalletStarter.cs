using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalletStarter : MonoBehaviour
{
    [SerializeField]
    private GameObject _walletPrefab;

    public void CreateWalletModel()
    {
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
