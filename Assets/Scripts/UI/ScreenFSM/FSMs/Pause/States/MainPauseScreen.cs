using UnityEngine;
using UnityEngine.UI;

public class MainPauseScreen : ScreenState
{
    [Header("Screens")]
    [SerializeField] private ScreenState _gameScreen;
    [SerializeField] private ScreenState _settingsScreen;

    [Header("Buttons")]
    [SerializeField] private Button _gameScreenButton;
    [SerializeField] private Button _settingsScreenButton;

    protected override void AwakeComponent()
    {
        if (_gameScreen == null || _settingsScreen == null)
            Debug.LogError($"You forgot to add ScreenState to the GameScreen script for the {name} object");

        if (_gameScreenButton == null || _settingsScreenButton == null)
            Debug.LogError($"You forgot to add Button to the GameScreen script for the {name} object");

        base.AwakeComponent();
    }

    public override void Entry()
    {
        base.Entry();

        _gameScreenButton.onClick.AddListener(() => Pause.Instance.ChangeScreen(_gameScreen));
        _settingsScreenButton.onClick.AddListener(() => Pause.Instance.ChangeScreen(_settingsScreen));
    }

    public override void Exit()
    {
        _gameScreenButton.onClick.RemoveListener(() => Pause.Instance.ChangeScreen(_gameScreen));
        _settingsScreenButton.onClick.RemoveListener(() => Pause.Instance.ChangeScreen(_settingsScreen));

        base.Exit();
    }
}
