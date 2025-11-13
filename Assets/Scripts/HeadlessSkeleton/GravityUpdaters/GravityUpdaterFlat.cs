using UnityEngine;

public class GravityUpdaterFlat : GravityUpdaterStrategy
{
    public override void OnGrounding(Vector3 normal)
    {
        Vector3 newGravity = (normal).normalized * _gravityValue;
        Physics.gravity = newGravity;
    }
}

