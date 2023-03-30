using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class FishnetPathRenderer : MonoBehaviour
{
    private LineRenderer _lineRenderer;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    private void Start()
    {
        PlayerInput playerInput = PlayerInput.Instance; 
        playerInput.OnScreenTouched += PlayerInput_OnScreenTouched;
        playerInput.OnScreenUntouched += PlayerInput_OnScreenUntouched;

        Hide();
    }

    private void PlayerInput_OnScreenTouched(object sender, PlayerInput.OnFingerMoovingEventArgs e) => Show();
    private void PlayerInput_OnScreenUntouched(object sender, PlayerInput.OnFingerMoovingEventArgs e) => Hide();

    private void Hide() => _lineRenderer.enabled = false;

    private void Show() => _lineRenderer.enabled = true;

    public void ReDraw(Vector3[] positions)
    {
        if (positions != null)
        {
            _lineRenderer.positionCount = positions.Length;
            _lineRenderer.SetPositions(positions);
        }
    }

    private void OnDestroy()
    {
        PlayerInput playerInput = PlayerInput.Instance;
        playerInput.OnScreenTouched -= PlayerInput_OnScreenTouched;
        playerInput.OnScreenUntouched -= PlayerInput_OnScreenUntouched;
    }
}
