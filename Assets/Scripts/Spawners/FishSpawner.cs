using UnityEngine;

public class FishSpawner : EntryLeaf
{
    [SerializeField]
    private GameObject _fishPrefab;

    [SerializeField][Range(.001f, 5f)]
    private float _timeTick;
    private float _currentTimeTick = 0f;

    [SerializeField]
    private CameraAnalizer _camera;

    [SerializeField][Range(0, 100)]
    private int _reward;
    [SerializeField]
    [Range(2, 10)]
    private float _fishMaxSpeed = 2f;
    [SerializeField]
    [Range(0, 5)]
    private float _fishMinSpeed = 1f;
    [SerializeField]
    private Vector2 _fishMaxDeviationFromTheMovingDirection;
    [SerializeField]
    private Vector2 _fishMinDeviationFromTheMovingDirection;

    protected override void StartComponent()
    {
        _fishMaxSpeed = _fishMinSpeed > _fishMaxSpeed ? _fishMinSpeed : _fishMaxSpeed;
        _fishMaxDeviationFromTheMovingDirection.x = _fishMaxDeviationFromTheMovingDirection.x < _fishMinDeviationFromTheMovingDirection.x ? _fishMinDeviationFromTheMovingDirection.x : _fishMaxDeviationFromTheMovingDirection.x;
        _fishMaxDeviationFromTheMovingDirection.x = _fishMaxDeviationFromTheMovingDirection.y < _fishMinDeviationFromTheMovingDirection.y ? _fishMinDeviationFromTheMovingDirection.y : _fishMaxDeviationFromTheMovingDirection.y;

        base.StartComponent();
    }

    protected override void UpdateComponent()
    {
        _currentTimeTick -= Time.deltaTime;
        if (_currentTimeTick <= 0)
        {
            SpawnFish();
            _currentTimeTick = _timeTick;
        }

        base.UpdateComponent();
    }

    private void SpawnFish()
    {
        Vector2 randomPointInSphere = Random.insideUnitCircle.normalized;
        Vector3 spawnPosition = new Vector3(randomPointInSphere.x, 0f, randomPointInSphere.y) * _camera.SpawnRadius;
        GameObject fish = Instantiate(_fishPrefab, spawnPosition, Quaternion.identity, transform);

        if (!fish.TryGetComponent(out FishView fishView))
            Debug.LogError("Fish prefab on " + gameObject.name + " dont have FishView");

        FishModel fishModel = new FishModel(_fishMinSpeed, _fishMaxSpeed, _fishMinDeviationFromTheMovingDirection, _fishMaxDeviationFromTheMovingDirection, spawnPosition, _reward);
        new FishPresenter(fishView, fishModel);
    }
}
