using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_LifePanel : MonoBehaviour
{
    [SerializeField] private GameObject _heartPrefab; // Prefab con Image
    [SerializeField] private Transform _heartContainer; // Contenitore dei cuori

    private List<Image> _hearts = new();

    [SerializeField] private Color _fullColor = Color.white;
    [SerializeField] private Color _emptyColor = Color.gray;

    public void UpdateLifeDisplay(int currentHp, int maxHp)
    {
        // Se il numero di cuori è diverso da quello richiesto, ricreali
        if (_hearts.Count != maxHp)
        {
            ResetHearts();
            for (int i = 0; i < maxHp; i++)
            {
                GameObject heart = Instantiate(_heartPrefab, _heartContainer);
                Image img = heart.GetComponent<Image>();
                _hearts.Add(img);
            }
        }

        // Aggiorna il colore in base alla vita corrente
        for (int i = 0; i < _hearts.Count; i++)
        {
            _hearts[i].color = i < currentHp ? _fullColor : _emptyColor;
        }
    }

    private void ResetHearts()
    {
        foreach (var heart in _hearts)
        {
            Destroy(heart.gameObject);
        }
        _hearts.Clear();
    }
}
