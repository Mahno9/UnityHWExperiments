using System;

using UnityEngine;

public class Health : BaseValueContainer
{
    [SerializeField] private float _startHealth = 10f;
    [SerializeField] private float _maxHealth = 100f;

    private float _health;

    private void Awake()
    {
        _health = _startHealth;
    }

    internal void AddHealth(float healthDelta)
    {
        _health += healthDelta;
    }

    public override float GetValue()
    {
        return _health;
    }

    public override float GetValueMax()
    {
        return _maxHealth;
    }
}