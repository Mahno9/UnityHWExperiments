using UnityEngine;

public class GameStopper : MonoBehaviour
{
    [SerializeField] private BaseValueContainer _gameState;

    private void Update()
    {
        if (_gameState.GetValue() <= 0 || _gameState.GetValue() >= _gameState.GetValueMax())
        {
            Time.timeScale = 0;
        }
    }
}