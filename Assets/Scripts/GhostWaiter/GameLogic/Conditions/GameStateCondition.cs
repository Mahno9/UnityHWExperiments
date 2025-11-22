using UnityEngine;

public class GameStateCondition: MonoBehaviour
{
    [SerializeField] private Health _health;

    public GameState GetState()
    {
        float hp = _health.GetValue();
        float maxHp = _health.GetValueMax();

        Debug.Log($"Current hp = {hp} and maxHp = {maxHp}");

        if (hp <= 0)
            return GameState.Lose;

        if (hp >= maxHp)
            return GameState.Win;

        return GameState.Playing;
    }
}