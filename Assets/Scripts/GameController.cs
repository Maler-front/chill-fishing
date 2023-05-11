public class GameController : EntryPoint
{
    public static GameController Instance { get; private set; }

    protected override void AwakeComponent()
    {
        if (Instance == null)
            Instance = this;

        base.AwakeComponent();
    }

    private void OnDestroy()
    {
        Instance = null;
        base.DisableComponent();
    }
}
