using UnityEngine;

public class FishnetSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject _fishnetPrefab;

    public GameObject CreateFishnet(bool isFishnetNeedToBeSimuleted = false)
    {
        Vector3 origin = transform.position;
        return isFishnetNeedToBeSimuleted ? Instantiate(_fishnetPrefab, origin, Quaternion.identity) :
                                            Instantiate(_fishnetPrefab, origin, Quaternion.identity, transform);
    }
}
