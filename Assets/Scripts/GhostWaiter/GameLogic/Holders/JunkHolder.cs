using UnityEngine;

public class JunkHolder : Holder
{
    public override Transform GetJointTransform()
    {
        return transform;
    }
}