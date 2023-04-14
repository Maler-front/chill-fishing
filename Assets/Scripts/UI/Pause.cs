using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    [SerializeField]
    private GameObject _pauseButtonObject;
    [SerializeField]
    private GameObject _pauseScreen;
    [SerializeField]
    private GameObject _settingsScreen;
    [SerializeField]
    private Button _pauseButton;
    [SerializeField]
    private Button _backToGameButton;
    [SerializeField]
    private Button _settingsButton;
    [SerializeField]
    private Button _closeSettingsButton;

    public UIScreen CurrentScreen { get; private set; }

    public void SubscribeToButtons()
    {
        OpenUIScreen(UIScreen.None);

        _pauseButton.onClick.AddListener(OpenPauseScreen);
        _backToGameButton.onClick.AddListener(ClosePauseScreen);
        _settingsButton.onClick.AddListener(OpenSettingsScreen);
        _closeSettingsButton.onClick.AddListener(OpenPauseScreen);
    }

    public void ClosePauseScreen()
    {
        OpenUIScreen(UIScreen.None);
        StartCoroutine(SetPauseState(false));
    }

    public void OpenPauseScreen()
    {
        OpenUIScreen(UIScreen.Pause);
        StartCoroutine(SetPauseState(true));
    }

    public void OpenSettingsScreen()
    {
        OpenUIScreen(UIScreen.Settings);
    }

    private void OpenUIScreen(UIScreen screen)
    {
        if (screen == CurrentScreen)
            return;

        SetActiveScreen(CurrentScreen, false);

        SetActiveScreen(screen, true);
    }

    private void SetActiveScreen(UIScreen screen, bool active)
    {
        switch (screen)
        {
            case UIScreen.None:
                {
                    _pauseButtonObject.SetActive(active);
                    break;
                }
            case UIScreen.Pause:
                {
                    _pauseScreen.SetActive(active);
                    break;
                }
            case UIScreen.Settings:
                {
                    _settingsScreen.SetActive(active);
                    break;
                }
        }

        if (active)
            CurrentScreen = screen;
    }

    private System.Collections.IEnumerator SetPauseState(bool paused)
    {
        if (paused)
        {
            GameController.Paused = paused;
        }
        else
        {
            yield return new WaitForEndOfFrame();
            GameController.Paused = paused;
        }

        yield return null;
    }

    public enum UIScreen
    {
        None, 
        Pause,
        Settings
    }
}
