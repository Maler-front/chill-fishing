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

    private void FishView_OnDie()
    {
        _fishView.Die();
        Remove();
    }

    private void FishView_NeedToMove()
    {
        _fishView.Move(_fishModel.GetChangePosition());
    }

    private void FishView_OnFishCatched()
    {
        WalletModel.Instance.AddCoins(_fishModel.Reward);
    }

    public void Activate()
    {
        _fishView.OnDie += FishView_OnDie;
        _fishView.NeedToMove += FishView_NeedToMove;
        _fishView.OnFishCatched += FishView_OnFishCatched;
    }

    public void Deactivate()
    {
        _fishView.OnDie -= FishView_OnDie;
        _fishView.NeedToMove -= FishView_NeedToMove;
        _fishView.OnFishCatched -= FishView_OnFishCatched;
    }

    public void Remove()
    {
        Deactivate();
        _fishModel = null;
        _fishView = null;
    }
}
