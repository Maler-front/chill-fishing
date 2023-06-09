using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Fishnet : MonoBehaviour
{
    public Rigidbody Rigidbody { get; private set; }

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if(transform.position.y < -5f) Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.WATER))
        {
            Rigidbody.drag = 3f;
        }
    }
}
