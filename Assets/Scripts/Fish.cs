using UnityEngine;

public class Fish : MonoBehaviour
{
    public float Speed { get; private set; }
    [SerializeField][Range(2, 10)]
    private float _maxSpeed;
    [SerializeField][Range(0, 5)]
    private float _minSpeed;
    public Vector2 MovingDirection { get; private set; }
    [SerializeField]
    private Vector2 _maxDeviationFromTheMovingDirection;
    [SerializeField]
    private Vector2 _minDeviationFromTheMovingDirection;

    private void Start()
    {
        _maxSpeed = _minSpeed > _maxSpeed ? _minSpeed : _maxSpeed;
        Speed = Random.Range(_minSpeed, _maxSpeed);

        Vector3 movingDirection3D = Vector3.zero - transform.position;
        MovingDirection = new Vector2(movingDirection3D.x, movingDirection3D.z);

        _maxDeviationFromTheMovingDirection.x = _maxDeviationFromTheMovingDirection.x < _minDeviationFromTheMovingDirection.x ? _minDeviationFromTheMovingDirection.x : _maxDeviationFromTheMovingDirection.x;
        _maxDeviationFromTheMovingDirection.x = _maxDeviationFromTheMovingDirection.y < _minDeviationFromTheMovingDirection.y ? _minDeviationFromTheMovingDirection.y : _maxDeviationFromTheMovingDirection.y;
        float deviationX = Random.Range(_minDeviationFromTheMovingDirection.x, _maxDeviationFromTheMovingDirection.x);
        float deviationY = Random.Range(_minDeviationFromTheMovingDirection.y, _maxDeviationFromTheMovingDirection.y);
        Vector2 deviation = new Vector2(deviationX, deviationY);
        MovingDirection += deviation;
        transform.forward = new Vector3(MovingDirection.x, 0, MovingDirection.y).normalized;
    }

    private void Update()
    {
        HandleMove();
        if (NeedToDie())
            Destroy(gameObject);
    }

    private void HandleMove()
    {
        Vector3 movingDirection3D = new Vector3(MovingDirection.x, 0, MovingDirection.y).normalized;
        transform.position += movingDirection3D * Speed * Time.deltaTime;
    }

    private bool NeedToDie()
    {
        return transform.position.magnitude > FishSpawner.MaxSpawnRadius;
    }
}
