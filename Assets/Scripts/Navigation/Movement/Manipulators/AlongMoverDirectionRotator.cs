using Navigation.Interfaces;

using UnityEngine;

public class AlongMoverDirectionRotator : IUpdatable
{
    private readonly IMovable   _mover;
    private readonly IRotatable _rotator;

    public float RotationSpeed => _rotator.RotationSpeed;

    public AlongMoverDirectionRotator(IMovable mover, IRotatable rotator)
    {
        _mover = mover;
        _rotator = rotator;
    }

    public void Update(float deltaTime)
    {
        Vector3 lookPoint = _mover.MoveDirection;

        _rotator.SetLookDirection(lookPoint.normalized);
        _rotator.Update(deltaTime);
    }
}