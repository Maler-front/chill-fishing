using System;
using UnityEngine;

public class FishesSystem : MonoBehaviour, ISystem
{
    [SerializeField]
    private FishSpawner _fishSpawner;

    public void Subscribe()
    {
        GameController gameController = GameController.Instance;
        gameController.OnStart += GameController_OnStart;
        gameController.OnUpdate += GameController_OnUpdate;
    }

    private void GameController_OnStart(object sender, EventArgs e)
    {
        _fishSpawner.FixSerializedData();

        GameController.Instance.OnStart -= GameController_OnStart;
    }

    private void GameController_OnUpdate(object sender, EventArgs e)
    {
        _fishSpawner.CheckSpawn();
    }
}
