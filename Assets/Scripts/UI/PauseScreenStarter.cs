using UnityEngine;

public class PauseScreenStarter : MonoBehaviour
{
    [SerializeField]
    private GameObject _pauseScreenPrefab;

    private GameObject pauseScreen;

    public void CreatePauseScreen()
    {
        pauseScreen = Instantiate(_pauseScreenPrefab, transform.parent);
    }

    public void ActivatePauseScreen()
    {
        pauseScreen.GetComponent<Pause>().SubscribeToButtons();
        Destroy(gameObject);
    }
}
