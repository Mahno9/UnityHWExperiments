using System;

using UnityEngine;

public class WinLoseShower : MonoBehaviour
{
    [SerializeField] private GameStateCondition _gameState;

    [SerializeField] private RectTransform _winContainer;
    [SerializeField] private RectTransform _loseContainer;

    private void Update()
    {
        switch (_gameState.GetState())
        {
            case GameState.Lose:
                _loseContainer.gameObject.SetActive(true);
                enabled = false;
                break;

            case GameState.Win:
                _winContainer.gameObject.SetActive(true);
                enabled = false;
                break;
        }
    }
}