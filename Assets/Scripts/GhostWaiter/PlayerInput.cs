using UnityEngine;
using UnityEngine.Assertions;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private CharacterMover _characterMover;
    [SerializeField] private Camera _camera;

    private const float _inputDeadZone = 0.1f;
    private const string HorizontalAxisName = "Horizontal";
    private const string VerticalAxisName = "Vertical";

    private void Update()
    {
        ProcessMoveInput();
    }

    private void ProcessMoveInput()
    {
        Assert.IsNotNull(_characterMover, "_characterMover is not assigned in the inspector.");

        Vector3 input = new Vector3(Input.GetAxisRaw(HorizontalAxisName), 0, Input.GetAxisRaw(VerticalAxisName));
        if (input.magnitude < _inputDeadZone)
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
