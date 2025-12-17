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

    public void  Update(float deltaTime)
    {
        Vector3 lookPoint = _mover.MoveDirection + _mover.Position;
        _rotator.SetLookPoint(lookPoint);
    }
}