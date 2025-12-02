using UnityEngine;

public class GameStateCondition: MonoBehaviour
{
    [SerializeField] private Health _health;

    public GameState GetState()
    {
        float hpNormalized = _health.GetProgress();
        const float maxNormalizedVal = 1f;
        const float minNormalizedVal = 0f;

        Debug.Log($"Current hp progress = {hpNormalized}");

        if (hpNormalized <= minNormalizedVal)
            return GameState.Lose;

        if (hpNormalized >= maxNormalizedVal)
            return GameState.Win;

        return GameState.Playing;
    }
}