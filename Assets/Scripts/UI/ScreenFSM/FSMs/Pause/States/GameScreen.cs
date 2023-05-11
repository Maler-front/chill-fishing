using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameScreen : ScreenState
{
    [SerializeField] private ScreenState _mainPauseScreen;
    [SerializeField] private Button _mainPauseScreenButton;

    [SerializeField] private Timer _timer;

    protected override void AwakeComponent()
    {
        if (_mainPauseScreen == null)
            Debug.LogError($"You forgot to add ScreenState to the GameScreen script for the {name} object");

        if(_mainPauseScreenButton == null)
            Debug.LogError($"You forgot to add Button to the GameScreen script for the {name} object");

        if (_timer == null)
            Debug.LogError($"You forgot to add Timer to the GameScreen script for the {name} object");

        base.AwakeComponent();
    }

    public override void Entry()
    {
        base.Entry();

        StartCoroutine(SetTimer());

        _mainPauseScreenButton.onClick.AddListener(() => Pause.Instance.ChangeScreen(_mainPauseScreen));
    }

    public override void Exit()
    {
        _mainPauseScreenButton.onClick.RemoveListener(() => Pause.Instance.ChangeScreen(_mainPauseScreen));
        Pause.Instance.Paused = true;

        base.Exit();
    }

    private IEnumerator SetTimer()
    {
        _timer.gameObject.SetActive(true);
        yield return StartCoroutine(_timer.TimerCountDown());
        _timer.gameObject.SetActive(false);
        Pause.Instance.Paused = false;

        yield return new WaitForEndOfFrame();
    }
}
