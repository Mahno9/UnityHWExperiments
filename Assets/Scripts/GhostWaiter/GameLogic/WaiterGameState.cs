using UnityEngine;

public class WaiterGameState : MonoBehaviour
{
    [SerializeField] private float _startHealth = 100f;

    private float _health = 100f;

    internal void AddHealth(float healthDelta)
    {
        _health += healthDelta;
    }
}