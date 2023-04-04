using UnityEngine;

[RequireComponent(typeof(FishnetFactory), typeof(LineRenderer))]
public class FishnetGunPresenter : MonoBehaviour
{
    [SerializeField]
    [Range(0.01f, 0.5f)]
    private float _simulationDeltaTime;
    [SerializeField]
    private float _maxForce;
    [SerializeField]
    private float _minForce;
    [SerializeField]
    private float _forceCoefficientForPlayer;
    [SerializeField]
    [Range(0.01f, 89.99f)]
    private float _fireAngle;

    private FishnetGunModel _fishnetGunModel;
    private FishnetGunView _fishnetGunView;
    private FishnetFactory _fishnetFactory;

    private void Start()
    {
        _fishnetFactory = GetComponent<FishnetFactory>();
        _fishnetGunView = gameObject.AddComponent<FishnetGunView>();
        _fishnetGunModel = new FishnetGunModel(
            _simulationDeltaTime,
            _maxForce,
            _minForce,
            _forceCoefficientForPlayer,
            _fireAngle
            );
        Activate();
    }

    private void OnDisable()
    {
        Deactivate();
    }

    private void Activate()
    {
        PlayerInputModel playerInput = PlayerInputModel.Instance;
        playerInput.OnFingerMoved += PlayerInputModel_OnFingerMoved;
        playerInput.OnScreenUntouched += PlayerInputModel_OnScreenUntouched;
        playerInput.OnScreenTouched += PlayerInputModel_OnScreenTouched;

        _fishnetGunModel.OnSimulationStart += FishnetGunModel_OnSimulationStart;
        _fishnetGunModel.OnSimulationEnd += FishnetGunModel_OnSimulationEnd;
    }

    private void Deactivate()
    {
        PlayerInputModel playerInput = PlayerInputModel.Instance;
        playerInput.OnFingerMoved -= PlayerInputModel_OnFingerMoved;
        playerInput.OnScreenUntouched -= PlayerInputModel_OnScreenUntouched;
        playerInput.OnScreenTouched -= PlayerInputModel_OnScreenTouched;

        _fishnetGunModel.OnSimulationStart -= FishnetGunModel_OnSimulationStart;
        _fishnetGunModel.OnSimulationEnd -= FishnetGunModel_OnSimulationEnd;
    }

    private void PlayerInputModel_OnFingerMoved(object sender, PlayerInputModel.OnFingerMoovingEventArgs e)
    {
        GameObject fishnet = _fishnetFactory.CreateFishnet(isFishnetNeedToBeSimuleted: true);
        _fishnetGunView.ReDraw(
            _fishnetGunModel.SimulateFishnetPath(
                fishnet,
                e
                )
            );
        Destroy(fishnet);
    }

    private void PlayerInputModel_OnScreenUntouched(object sender, PlayerInputModel.OnFingerMoovingEventArgs e)
    {
        _fishnetGunView.Hide();
        GameObject fishnet = _fishnetFactory.CreateFishnet();
        _fishnetGunModel.Fire(fishnet, e);
    }

    private void PlayerInputModel_OnScreenTouched(object sender, PlayerInputModel.OnFingerMoovingEventArgs e)
    {
        _fishnetGunView.Show();
    }

    private void FishnetGunModel_OnSimulationStart(object sender, System.EventArgs e)
    {
        _fishnetGunView.BakeFishnetPositions();
    }

    private void FishnetGunModel_OnSimulationEnd(object sender, System.EventArgs e)
    {
        _fishnetGunView.UnbakeFishnetPositions();
    }
}
