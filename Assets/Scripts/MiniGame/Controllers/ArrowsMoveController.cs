using System.Collections.Generic;
using System.Linq;

using Navigation.Controllers;

using UnityEngine;

namespace MiniGame
{
    public class ArrowsMoveController : ControllerBase
    {
        private readonly Dictionary<KeyCode, Vector3> _keyDirections = new()
        {
            { KeyCode.A, Vector3.left },
            { KeyCode.D, Vector3.right },
            { KeyCode.W, Vector3.forward },
            { KeyCode.S, Vector3.back }
        };

        private readonly ISimpleMovable _movable;
        private readonly float          _moveSpeed;

        public ArrowsMoveController(ISimpleMovable movable, float moveSpeed)
        {
            _movable = movable;
            _moveSpeed = moveSpeed;
        }

        protected override void UpdateLogic(float deltaTime)
        {
            Vector3 moveInput = ProcessInput();
            _movable.Move(moveInput * (_moveSpeed * deltaTime));
        }

        private Vector3 ProcessInput()
        {
            return _keyDirections
                .Where(keyDirection => Input.GetKey(keyDirection.Key))
                .Aggregate<KeyValuePair<KeyCode, Vector3>, Vector3>(default, (current, keyDirection) => current + keyDirection.Value)
                .normalized;
        }
    }
}