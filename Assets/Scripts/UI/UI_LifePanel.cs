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
        if (_hearts.Count != maxHp)
        {
            ResetHearts();

            // Ricreazione dei cuori
            for (int i = 0; i < maxHp; i++)
            {
                GameObject heart = Instantiate(_heartPrefab, _heartContainer);
                Image img = heart.GetComponent<Image>();

                if (img != null)
                {
                    img.color = i < currentHp ? _fullColor : _emptyColor;
                    _hearts.Add(img);
                }
            }
        }
        else
        {
            // Se la lista è valida e già della giusta dimensione, aggiorna i colori
            for (int i = 0; i < _hearts.Count; i++)
            {
                if (_hearts[i] != null)
                    _hearts[i].color = i < currentHp ? _fullColor : _emptyColor;
            }
        }
    }


    private void ResetHearts()
    {
        foreach (var heart in _hearts)
        {
            if (heart != null)
                Destroy(heart.gameObject);
        }
        _hearts.Clear();
    }
}
