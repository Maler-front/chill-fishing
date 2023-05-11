using System;
using UnityEngine;

public class FishView : MonoBehaviour
{
    [SerializeField] private CameraAnalizer _cameraAnalizer;

    public Action OnDie;
    public Action NeedToMove;
    public Action OnFishCatched;

    public void Move(Vector3 changePosition)
    {
        transform.forward = changePosition.normalized;
        transform.Translate(transform.InverseTransformVector(changePosition * Time.fixedDeltaTime));
    }

    private void FixedUpdate()
    {
        NeedToMove?.Invoke();
        if (NeedToDie())
            OnDie?.Invoke();
    }

    private bool NeedToDie()
    {
        return false;
        /*return transform.position.magnitude > _cameraAnalizer.SpawnRadius;*/
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Fishnet fishnet))
        {
            OnFishCatched?.Invoke();
            OnDie?.Invoke();
        }
    }
}
