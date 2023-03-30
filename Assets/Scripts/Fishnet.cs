using UnityEngine;

public class Fishnet : MonoBehaviour
{
    private void Update()
    {
        if(transform.position.y < -1f) Destroy(gameObject);
    }
}
