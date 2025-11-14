using UnityEngine;

public class GravityUpdaterRound : GravityUpdaterStrategy
{
    public override void OnUpdate(Vector3 mainObjectPos)
    {
        Vector3 newGravity = (transform.position - mainObjectPos).normalized * _gravityValue;
        Physics.gravity = newGravity;
    }
}

