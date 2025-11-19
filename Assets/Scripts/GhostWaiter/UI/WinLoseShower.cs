using UnityEngine;

public class WinLoseShower : MonoBehaviour
{
    [SerializeField] private BaseValueContainer _gameState;

    [SerializeField] private RectTransform _winContainer;
    [SerializeField] private RectTransform _loseContainer;

    private void Update()
    {
        if (_gameState.GetValue() <= 0)
        {
            _loseContainer.gameObject.SetActive(true);
            enabled = false;
        }
        else if (_gameState.GetValue() >= _gameState.GetValueMax())
        {
            _winContainer.gameObject.SetActive(true);
            enabled = false;
        }
    }
}