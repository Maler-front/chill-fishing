using UnityEngine;
using UnityEngine.UI;

public class SettingsScreen : ScreenState
{
    [SerializeField] private ScreenState _mainPauseScreen;
    [SerializeField] private Button _mainPauseScreenButton;

    protected override void AwakeComponent()
    {
        if (_mainPauseScreen == null)
            Debug.LogError($"You forgot to add ScreenState to the SettingsScreen script for the {name} object");

        if (_mainPauseScreenButton == null)
            Debug.LogError($"You forgot to add Button to the SettingsScreen script for the {name} object");

        base.AwakeComponent();
    }

    public override void Entry()
    {
        base.Entry();

        _mainPauseScreenButton.onClick.AddListener(() => Pause.Instance.ChangeScreen(_mainPauseScreen));
    }

    public override void Exit()
    {
        _mainPauseScreenButton.onClick.RemoveListener(() => Pause.Instance.ChangeScreen(_mainPauseScreen));
        
        base.Exit();
    }
}
