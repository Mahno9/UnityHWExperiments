using System;

using UnityEngine;

public class Spinning : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 50;
    [SerializeField] private float _floatingSpeed = 3;
    [SerializeField] private float _floatingHeight = 0.15f;

    private Vector3 _initialPos;

    private void Awake()
    {
        _initialPos = transform.position;
    }

    private void Update()
    {
        UpdateRotation();
        UpdatePosition();
    }

    private void UpdateRotation()
    {
        transform.Rotate(Vector3.up, _rotationSpeed * Time.deltaTime, Space.World);
    }
    private void UpdatePosition()
    {
        const float SHIFT_TO_MINUS_ONE = -MathF.PI / 2; // To start from bottom position

        float yOffset = MathF.Sin(Time.time * _floatingSpeed + SHIFT_TO_MINUS_ONE) * _floatingHeight + _floatingHeight;
        transform.position = _initialPos + new Vector3(0, yOffset, 0);
    }
}
