using UnityEngine;

public abstract class ScreenFSM : EntryLeaf
{
    [SerializeField] private ScreenState _startScreen;
    protected ScreenState _currentScreen;

    protected override void StartComponent()
    {
        _currentScreen = _startScreen;
        _startScreen.Entry();

        base.StartComponent();
    }

    public abstract void ChangeScreen(ScreenState state);
}
