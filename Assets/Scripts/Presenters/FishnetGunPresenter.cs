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
        TouchController touchController = TouchController.Instance;
        touchController.OnFingerMoved += TouchControllerModel_OnFingerMoved;
        touchController.OnScreenUntouched += TouchControllerModel_OnScreenUntouched;
        touchController.OnScreenTouched += TouchControllerModel_OnScreenTouched;

        _fishnetGunModel.OnSimulationStart += FishnetGunModel_OnSimulationStart;
        _fishnetGunModel.OnSimulationEnd += FishnetGunModel_OnSimulationEnd;
    }

    public void Deactivate()
    {
        TouchController touchController = TouchController.Instance;
        touchController.OnFingerMoved -= TouchControllerModel_OnFingerMoved;
        touchController.OnScreenUntouched -= TouchControllerModel_OnScreenUntouched;
        touchController.OnScreenTouched -= TouchControllerModel_OnScreenTouched;

        _fishnetGunModel.OnSimulationStart -= FishnetGunModel_OnSimulationStart;
        _fishnetGunModel.OnSimulationEnd -= FishnetGunModel_OnSimulationEnd;
    }

    private void TouchControllerModel_OnFingerMoved(object sender, TouchController.OnFingerMoovingEventArgs e)
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

    private void TouchControllerModel_OnScreenUntouched(object sender, TouchController.OnFingerMoovingEventArgs e)
    {
        _fishnetGunView.Hide();
        GameObject fishnet = _fishnetSpawner.CreateFishnet();
        _fishnetGunModel.Fire(fishnet, e);
    }

    private void TouchControllerModel_OnScreenTouched(object sender, TouchController.OnFingerMoovingEventArgs e)
    {
        _fishnetGunView.Show();

        TouchControllerModel_OnFingerMoved(sender, e);
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
