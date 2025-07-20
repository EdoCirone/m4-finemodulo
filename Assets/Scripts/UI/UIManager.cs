using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    [SerializeField] UI_CoinPanel _coinPanel;
    [SerializeField] UI_LifePanel _lifePanel;
    [SerializeField] private UI_Timer _timerPanel;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {

            Destroy(gameObject);
            return;

        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // per farlo restare tra le scene
    }
    public void ResetUI()
    {
        _coinPanel.UpdateCoinGraphics(0, 0);
        _lifePanel.UpdateLifeDisplay(0, 0);
        _timerPanel.UpdateTimerUI(0);
    }
    public void RefreshLifeFromPlayer(LifeController source)
    {
        UpdateLife(source.CurrentHp, source.MaxHp);
    }
    public void UpdateCoins(int current, int max) => _coinPanel.UpdateCoinGraphics(current, max);
    public void UpdateLife(int currentHp, int maxHp) => _lifePanel.UpdateLifeDisplay(currentHp, maxHp);


    public void UpdateTimer(float timeLeft) => _timerPanel.UpdateTimerUI(timeLeft);
}
