using System.Collections.Generic;
using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    [SerializeField]
    [Range(0,10)]
    private float _spawnSpace;

    [SerializeField]
    [Range(0, 3)]
    private float _indent;

    public static float MinSpawnRadius { get; private set; }
    public static float MaxSpawnRadius { get; private set; }

    [SerializeField]
    private List<GameObject> _fishPrefabs;

    [SerializeField][Range(.001f, 5f)]
    private float _timeTick;
    private float _currentTimeTick;

    private void Start()
    {
        Vector3 leftCorner = CameraAnalizer.GetLeftMainCameraCorner();
        Vector3 rightCorner = CameraAnalizer.GetRightMainCameraCorner();

        MinSpawnRadius = CameraAnalizer.GetMinRadius(leftCorner, rightCorner) + _indent;
        MaxSpawnRadius = MinSpawnRadius + _spawnSpace;
        _currentTimeTick = _timeTick;
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
        Vector3 spawnPosition = new Vector3(randomPointInSphere.x, 0f, randomPointInSphere.y) * Random.Range(MinSpawnRadius, MaxSpawnRadius);
        Instantiate(_fishPrefabs[0], spawnPosition, Quaternion.identity);
    }
}
