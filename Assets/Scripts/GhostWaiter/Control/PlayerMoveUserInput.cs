using GhostWaiter.GameLogic.Holders;

using UnityEngine;
using UnityEngine.Assertions;

namespace GhostWaiter.Control
{
    public class PlayerMoveUserInput : MonoBehaviour
    {
        [SerializeField] private User _user;

        private void Update()
        {
            ProcessUserInput();
        }

        private void ProcessUserInput()
        {
            Assert.IsNotNull(_user, "_user is not assigned in the inspector.");

            if (Input.GetButtonDown("Use"))
                _user.Use();
        }
    }
}