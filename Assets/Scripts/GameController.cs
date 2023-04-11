using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private Camera _camera;
    [SerializeField]
    private GameObject _walletPrefab;

    private void Awake()
    {
        CameraAnalizer.CalculateSpawnRadius(_camera);
        new WalletModel();
    }

    private void Start()
    {
        GameObject wallet = Instantiate(_walletPrefab, transform);
        WalletView walletView = wallet.GetComponent<WalletView>();
        new WalletPresenter(walletView);
    }
}
