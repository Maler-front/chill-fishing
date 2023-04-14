using System;
using UnityEngine;

public class UISystem : MonoBehaviour, ISystem
{
    [SerializeField]
    private WalletStarter _walletStarter;
    [SerializeField]
    private PauseScreenStarter _pauseScreenStarter;

    public void Subscribe()
    {
        GameController gameController = GameController.Instance;
        gameController.OnAwake += GameController_OnAwake;
        gameController.OnStart += GameController_OnStart;
    }

    private void GameController_OnAwake(object sender, EventArgs e)
    {
        _walletStarter.CreateWalletModel();
        _pauseScreenStarter.CreatePauseScreen();
        GameController.Instance.OnAwake -= GameController_OnAwake;
    }

    private void GameController_OnStart(object sender, EventArgs e)
    {
        _walletStarter.CreateWalletVP();
        _pauseScreenStarter.ActivatePauseScreen();
        GameController.Instance.OnStart -= GameController_OnStart;
    }
}
