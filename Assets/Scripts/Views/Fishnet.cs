using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Fishnet : MonoBehaviour
{
    private const string WATER_TAG = "Water";
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if(transform.position.y < -1f) Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(WATER_TAG))
            _rigidbody.drag = 5f;
    }
}
