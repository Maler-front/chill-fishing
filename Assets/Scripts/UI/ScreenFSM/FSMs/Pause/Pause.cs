using System;
using UnityEngine;

public class Pause : ScreenFSM
{
    public static Pause Instance { get; private set; }

    private bool _paused = false;
    public bool Paused
    {
        get => _paused;
        set
        {
            _paused = value;
            PauseStateChange?.Invoke(value);
            if (_paused)
                Time.timeScale = 0;
            else
                Time.timeScale = 1;
        }
    }
    public Action<bool> PauseStateChange;

    protected override void AwakeComponent()
    {
        Instance = this;
        Paused = false;

        base.AwakeComponent();
    }

    public override void ChangeScreen(ScreenState state)
    {
        _currentScreen.Exit();
        _currentScreen = state;
        _currentScreen.Entry();
    }

    private void OnDestroy()
    {
        Instance = null;
        base.DisableComponent();
    }
}
