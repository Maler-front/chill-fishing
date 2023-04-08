using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    [SerializeField]
    private Camera _camera;

    [SerializeField]
    private GameObject _fishPrefab;

    [SerializeField][Range(.001f, 5f)]
    private float _timeTick;
    private float _currentTimeTick = 0f;

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

    private void Start()
    {
        _fishMaxSpeed = _fishMinSpeed > _fishMaxSpeed ? _fishMinSpeed : _fishMaxSpeed;
        _fishMaxDeviationFromTheMovingDirection.x = _fishMaxDeviationFromTheMovingDirection.x < _fishMinDeviationFromTheMovingDirection.x ? _fishMinDeviationFromTheMovingDirection.x : _fishMaxDeviationFromTheMovingDirection.x;
        _fishMaxDeviationFromTheMovingDirection.x = _fishMaxDeviationFromTheMovingDirection.y < _fishMinDeviationFromTheMovingDirection.y ? _fishMinDeviationFromTheMovingDirection.y : _fishMaxDeviationFromTheMovingDirection.y;

        CameraAnalizer.CalculateSpawnRadius(_camera);
    }

    private void Update()
    {
        _currentTimeTick -= Time.deltaTime;
        if(_currentTimeTick <= 0)
        {
            SpawnFish();
            _currentTimeTick = _timeTick;
        }
    }

    private void SpawnFish()
    {
        Vector2 randomPointInSphere = Random.insideUnitCircle.normalized;
        Vector3 spawnPosition = new Vector3(randomPointInSphere.x, 0f, randomPointInSphere.y) * CameraAnalizer.SpawnRadius;
        GameObject fish = Instantiate(_fishPrefab, spawnPosition, Quaternion.identity, transform);

        if (!fish.TryGetComponent(out FishView fishView))
            Debug.LogError("Fish prefab on " + gameObject.name + " dont have FishView");

        FishModel fishModel = new FishModel(_fishMinSpeed, _fishMaxSpeed, _fishMinDeviationFromTheMovingDirection, _fishMaxDeviationFromTheMovingDirection, spawnPosition);
        new FishPresenter(fishView, fishModel);
    }
}
