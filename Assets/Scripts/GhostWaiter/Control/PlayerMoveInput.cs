using UnityEngine;
using UnityEngine.Assertions;

namespace GhostWaiter.Control
{
    public class PlayerMoveInput : MonoBehaviour
    {
        [SerializeField] private CharacterMover _characterMover;
        [SerializeField] private Camera _camera;

        private const float INPUT_DEAD_ZONE = 0.1f;
        private const string HORIZONTAL_AXIS_NAME = "Horizontal";
        private const string VERTICAL_AXIS_NAME = "Vertical";

        private void Update()
        {
            ProcessMoveInput();
        }

        private void ProcessMoveInput()
        {
            Assert.IsNotNull(_characterMover, "_characterMover is not assigned in the inspector.");

            Vector3 input = new Vector3(Input.GetAxisRaw(HORIZONTAL_AXIS_NAME), 0, Input.GetAxisRaw(VERTICAL_AXIS_NAME));
            if (input.magnitude < INPUT_DEAD_ZONE)
                return;

            Vector3 inputNormalized = input.normalized;
            inputNormalized = CompensateCamera(inputNormalized);

            _characterMover.ProcessMoveTo(inputNormalized);
            _characterMover.ProcessRotateTo(inputNormalized);
        }

        private Vector3 CompensateCamera(Vector3 v)
        {
            return Quaternion.AngleAxis(_camera.transform.eulerAngles.y, Vector3.up) * v;
        }
    }
}