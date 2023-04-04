using UnityEngine;

public class FishnetFactory : MonoBehaviour
{
    [SerializeField]
    private GameObject _fishnetPrefab;

    public GameObject CreateFishnet(bool isFishnetNeedToBeSimuleted = false)
    {
        Vector3 origin = new Vector3(transform.parent.position.x, transform.parent.position.y + 1f, transform.parent.position.z);
        return isFishnetNeedToBeSimuleted ? Instantiate(_fishnetPrefab, origin, Quaternion.identity) :
                                            Instantiate(_fishnetPrefab, origin, Quaternion.identity, transform);
    }
}
