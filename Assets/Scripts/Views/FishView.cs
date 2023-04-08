using System;
using UnityEngine;

public class FishView : MonoBehaviour
{
    public EventHandler OnDie;
    public EventHandler NeedToMove;
    public EventHandler OnFishCatched;

    public void Move(Vector3 changePosition)
    {
        transform.forward = changePosition.normalized;
        transform.position += changePosition * Time.fixedDeltaTime;
    }

    private void FixedUpdate()
    {
        NeedToMove?.Invoke(this, EventArgs.Empty);
        if (NeedToDie())
            OnDie?.Invoke(this, EventArgs.Empty);
    }

    private bool NeedToDie()
    {
        return transform.position.magnitude > CameraAnalizer.SpawnRadius;
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Fishnet fishnet))
        {
            OnFishCatched?.Invoke(this, EventArgs.Empty);
            OnDie?.Invoke(this, EventArgs.Empty);
        }
    }
}
