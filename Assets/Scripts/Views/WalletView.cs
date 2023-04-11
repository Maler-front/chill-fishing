using TMPro;
using UnityEngine;

public class WalletView : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _textMesh;

    public void ChangeCoinsTo(int coins)
    {
        print(coins.ToString());
        _textMesh.text = coins.ToString();
    }
}
