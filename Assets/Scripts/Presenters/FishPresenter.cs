public class FishPresenter : IPresenter, IRemovable
{
    FishModel _fishModel;
    FishView _fishView;

    public FishPresenter(FishView fishView, FishModel fishModel)
    {
        _fishView = fishView;
        _fishModel = fishModel;

        Activate();
    }

    private void FishView_OnDie(object sender, System.EventArgs e)
    {
        _fishView.Die();
        Remove();
    }

    private void FishView_NeedToMove(object sender, System.EventArgs e)
    {
        _fishView.Move(_fishModel.GetChangePosition());
    }

    public void Activate()
    {
        _fishView.OnDie += FishView_OnDie;
        _fishView.NeedToMove += FishView_NeedToMove;
    }

    public void Deactivate()
    {
        _fishView.OnDie -= FishView_OnDie;
        _fishView.NeedToMove -= FishView_NeedToMove;
    }

    public void Remove()
    {
        Deactivate();
        _fishModel = null;
        _fishView = null;
    }
}
