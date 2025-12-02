using System;

using UnityEngine;

public class Health : Progressable
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

    public override float GetProgress()
    {
        return _health / _maxHealth;
    }
}