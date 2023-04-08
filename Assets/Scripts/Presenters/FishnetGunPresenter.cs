using System.Collections.Generic;
using UnityEngine;

public class FishnetGunPresenter : IPresenter, IRemovable
{
    private FishnetGunModel _fishnetGunModel;
    private FishnetGunView _fishnetGunView;
    private FishnetSpawner _fishnetSpawner;

    private GameObject _simulatingFishnet;

    public FishnetGunPresenter(FishnetGunView fishnetGunView, FishnetGunModel fishnetGunModel, FishnetSpawner fishnetSpawner)
    {
        _fishnetGunView = fishnetGunView;
        _fishnetGunModel = fishnetGunModel;
        _fishnetSpawner = fishnetSpawner;

        Activate();
    }

    public void Activate()
    {
        PlayerInput playerInput = PlayerInput.Instance;
        playerInput.OnFingerMoved += PlayerInputModel_OnFingerMoved;
        playerInput.OnScreenUntouched += PlayerInputModel_OnScreenUntouched;
        playerInput.OnScreenTouched += PlayerInputModel_OnScreenTouched;

        _fishnetGunModel.OnSimulationStart += FishnetGunModel_OnSimulationStart;
        _fishnetGunModel.OnSimulationEnd += FishnetGunModel_OnSimulationEnd;
    }

    public void Deactivate()
    {
        PlayerInput playerInput = PlayerInput.Instance;
        playerInput.OnFingerMoved -= PlayerInputModel_OnFingerMoved;
        playerInput.OnScreenUntouched -= PlayerInputModel_OnScreenUntouched;
        playerInput.OnScreenTouched -= PlayerInputModel_OnScreenTouched;

        _fishnetGunModel.OnSimulationStart -= FishnetGunModel_OnSimulationStart;
        _fishnetGunModel.OnSimulationEnd -= FishnetGunModel_OnSimulationEnd;
    }

    private void PlayerInputModel_OnFingerMoved(object sender, PlayerInput.OnFingerMoovingEventArgs e)
    {
        GameObject fishnet = _fishnetSpawner.CreateFishnet(isFishnetNeedToBeSimuleted: true);
        _simulatingFishnet = fishnet;

        _fishnetGunView.ReDraw(
            _fishnetGunModel.SimulateFishnetPath(
                fishnet,
                e
                )
            );
    }

    private void PlayerInputModel_OnScreenUntouched(object sender, PlayerInput.OnFingerMoovingEventArgs e)
    {
        _fishnetGunView.Hide();
        GameObject fishnet = _fishnetSpawner.CreateFishnet();
        _fishnetGunModel.Fire(fishnet, e);
    }

    private void PlayerInputModel_OnScreenTouched(object sender, PlayerInput.OnFingerMoovingEventArgs e)
    {
        _fishnetGunView.Show();

        PlayerInputModel_OnFingerMoved(sender, e);
    }

    private void FishnetGunModel_OnSimulationStart(object sender, System.EventArgs e)
    {
        _fishnetGunView.BakeFishnetPositions();
    }

    private void FishnetGunModel_OnSimulationEnd(object sender, System.EventArgs e)
    {
        _fishnetGunView.UnbakeFishnetPositions();
        _fishnetGunView.DeleteFishnet(_simulatingFishnet);
        _simulatingFishnet = null;
    }

    public void Remove()
    {
        Deactivate();
        _fishnetGunModel = null;
        _fishnetGunView = null;
        _fishnetSpawner = null;
    }
}
