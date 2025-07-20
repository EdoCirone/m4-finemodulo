using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timerText;

    public void UpdateTimerUI(float timeLeft)
    {
        int minutes = Mathf.FloorToInt(timeLeft / 60f);
        int seconds = Mathf.FloorToInt(timeLeft % 60f);
        _timerText.text = $"{minutes:00}:{seconds:00}";
    }

}
