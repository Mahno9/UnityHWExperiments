using UnityEngine;

public class GravityUpdaterRound : GravityUpdaterStrategy
{
    override public void OnUpdate(Vector3 mainObjectPos)
    {
        Vector3 newGravity = (transform.position - mainObjectPos).normalized * _gravityValue;
        Physics.gravity = newGravity;
    }
}

