using UnityEngine;

namespace GhostWaiter.GameLogic.Holders
{
    public abstract class Holder : MonoBehaviour
    {
        public abstract Transform GetJointTransform();
    }
}
