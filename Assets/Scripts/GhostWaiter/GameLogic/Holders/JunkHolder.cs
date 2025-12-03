using UnityEngine;

namespace GhostWaiter.GameLogic.Holders
{
    public class JunkHolder : Holder
    {
        public override Transform GetJointTransform()
        {
            return transform;
        }
    }
}