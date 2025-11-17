using UnityEngine;
using UnityEngine.Assertions;

public class UseHealthIncrease : Usable
{
    [SerializeField] private int _healthIncreaseAmount = 20;

    private WaiterGameState _gameState;

    public override void Use()
    {
        Assert.IsNotNull(_gameState);

        _gameState.AddHealth(_healthIncreaseAmount);

        base.Use();
    }

    public override void Init(WaiterGameState gameState)
    {
        _gameState = gameState;
    }
}
