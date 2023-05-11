using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField]
    private int _defaultSeconds;
    [SerializeField]
    private Text _text;

    public System.Collections.IEnumerator TimerCountDown(int seconds)
    {
        for (int i = seconds; i > 0; i--)
        {
            _text.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }
        yield break;
    }

    public System.Collections.IEnumerator TimerCountDown()
    {
        for (int i = _defaultSeconds; i > 0; i--)
        {
            _text.text = i.ToString();
            yield return new WaitForSecondsRealtime(1f);
        }
    }
}
