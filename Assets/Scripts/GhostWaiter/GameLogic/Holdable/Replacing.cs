using UnityEngine;

namespace GhostWaiter.GameLogic.Holdable
{
    public class Replacing : ThrowUsable
    {
        [SerializeField] private string _tag;

        public string Tag => _tag;
    }
}
