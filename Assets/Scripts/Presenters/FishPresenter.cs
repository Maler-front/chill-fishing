using UnityEngine;

public class FishPresenter : MonoBehaviour
{
    FishModel _fishModel;
    FishView _fishView;

    [SerializeField]
    [Range(2, 10)]
    private float _maxSpeed;
    [SerializeField]
    [Range(0, 5)]
    private float _minSpeed;
    [SerializeField]
    private Vector2 _maxDeviationFromTheMovingDirection;
    [SerializeField]
    private Vector2 _minDeviationFromTheMovingDirection;

    private void Start()
    {
        //Creating fishModel
        _maxSpeed = _minSpeed > _maxSpeed ? _minSpeed : _maxSpeed;
        float speed = Random.Range(_minSpeed, _maxSpeed);

        Vector3 movingDirection3D = Vector3.zero - transform.position;
        Vector2 movingDirection = new Vector2(movingDirection3D.x, movingDirection3D.z);

        _maxDeviationFromTheMovingDirection.x = _maxDeviationFromTheMovingDirection.x < _minDeviationFromTheMovingDirection.x ? _minDeviationFromTheMovingDirection.x : _maxDeviationFromTheMovingDirection.x;
        _maxDeviationFromTheMovingDirection.x = _maxDeviationFromTheMovingDirection.y < _minDeviationFromTheMovingDirection.y ? _minDeviationFromTheMovingDirection.y : _maxDeviationFromTheMovingDirection.y;
        float deviationX = Random.Range(_minDeviationFromTheMovingDirection.x, _maxDeviationFromTheMovingDirection.x);
        float deviationY = Random.Range(_minDeviationFromTheMovingDirection.y, _maxDeviationFromTheMovingDirection.y);
        Vector2 deviation = new Vector2(deviationX, deviationY);
        movingDirection += deviation;
        
        _fishModel = new FishModel(speed, movingDirection);
        _fishView = gameObject.AddComponent<FishView>();
        //---

        Activate();
    }

    private void FishView_OnDie(object sender, System.EventArgs e)
    {
        _fishView.Die();
    }

    private void FishView_NeedToMove(object sender, System.EventArgs e)
    {
        _fishView.Move(_fishModel.GetChangePosition());
    }

    private void Activate()
    {
        _fishView.OnDie += FishView_OnDie;
        _fishView.NeedToMove += FishView_NeedToMove;
    }

    private void Deactivate()
    {
        _fishView.OnDie -= FishView_OnDie;
        _fishView.NeedToMove -= FishView_NeedToMove;
    }

    private void OnDisable()
    {
        Deactivate();
    }
}
