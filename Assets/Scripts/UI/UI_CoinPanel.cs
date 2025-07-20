using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class UI_CoinPanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _coinText;

   public void UpdateCoinGraphics(int _playercoins, int maxCoins)
    {
        _coinText.text = _playercoins + "/" + maxCoins;
    }
}
