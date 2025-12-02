using UnityEngine;

public class GameStopper : MonoBehaviour
{
    [SerializeField] private GameStateCondition _gameState;

    private void Update()
    {
        if (_gameState.GetState() != GameState.Playing)
            Time.timeScale = 0;
    }
}