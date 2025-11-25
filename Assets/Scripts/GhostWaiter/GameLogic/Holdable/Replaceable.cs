using UnityEngine;

namespace GhostWaiter.GameLogic.Holdable
{
    public class Replaceable : Holdable
    {
        [SerializeField] private string _requiredTag;

        public string RequiredTag => _requiredTag;
    }
}
