using System;
using UnityEngine;

public class FishView : MonoBehaviour
{
    public EventHandler OnDie;
    public EventHandler NeedToMove;

    public void Move(Vector3 changePosition)
    {
        transform.forward = changePosition.normalized;
        //transform.Translate(changePosition * Time.fixedDeltaTime);
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
        return transform.position.magnitude > FishSpawner.MaxSpawnRadius;
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
