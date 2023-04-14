using System;
using UnityEngine;

public class InputSystem : MonoBehaviour, ISystem
{
    [SerializeField]
    private TouchController _touchController;
    [SerializeField]
    private FishnetGunStarter _fishnetGunStarter;

    public void Subscribe()
    {
        GameController gameController = GameController.Instance;
        gameController.OnAwake += GameController_OnAwake;
        gameController.OnStart += GameController_OnStart;
        gameController.OnUpdate += GameController_OnUpdate;
    }

    private void GameController_OnAwake(object sender, EventArgs e)
    {
        _touchController.CreateInstance();

        GameController.Instance.OnAwake -= GameController_OnAwake;
    }

    private void GameController_OnStart(object sender, EventArgs e)
    {
        _fishnetGunStarter.CreateFishnetGun();

        GameController.Instance.OnStart -= GameController_OnStart;
    }

    private void GameController_OnUpdate(object sender, EventArgs e)
    {
        _touchController.AnalizeTouch();
    }
}
