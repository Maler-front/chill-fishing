using UnityEngine;

public class FishnetGunStarter : EntryLeaf
{
    [SerializeField]
    private GameObject _fishnetGunPrefab;

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

    protected override void StartComponent()
    {
        CreateFishnetGun();

        base.StartComponent();
    }

    public void CreateFishnetGun()
    {
        GameObject fishnetGun = Instantiate(_fishnetGunPrefab, transform.parent.transform.position + new Vector3(0, 1f, 0), Quaternion.identity, transform.parent);
        if (!fishnetGun.TryGetComponent(out FishnetGunView fishnetGunView))
            Debug.LogError("FishnetGun prefab dont have FishnetGunView");
        if (!fishnetGun.TryGetComponent(out FishnetSpawner fishnetSpawner))
            Debug.LogError("FishnetGun prefab dont have FishnetSpawner");

        FishnetGunModel fishnetGunModel = new FishnetGunModel(
            _simulationDeltaTime,
            _maxForce,
            _minForce,
            _forceCoefficientForPlayer,
            _fireAngle
            );

        new FishnetGunPresenter(fishnetGunView, fishnetGunModel, fishnetSpawner);
        Destroy(gameObject);
    }
}
