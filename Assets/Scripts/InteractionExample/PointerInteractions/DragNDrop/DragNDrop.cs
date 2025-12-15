using UnityEngine;
using UnityEngine.Assertions;

namespace InteractionExample.PointerInteractions.DragNDrop
{
    public class DragNDrop : MonoBehaviour
    {
        private const int LeftMouseButton = 0;

        private Camera         _camera;
        private DragNDropService _service;

        private void Start()
        {
            _camera = Camera.main;
            Assert.IsNotNull(_camera);

            _service = new DragNDropService();
        }

        private void Update()
        {
            Ray pointerRay = _camera.ScreenPointToRay(Input.mousePosition);

            if (Input.GetMouseButtonDown(LeftMouseButton))
                _service.GrabItemOnRay(pointerRay);

            if (Input.GetMouseButtonUp(LeftMouseButton))
                _service.ReleaseItem();
        }

        private void FixedUpdate()
        {
            Ray pointerRay = _camera.ScreenPointToRay(Input.mousePosition);

            _service.SetItemPositionByRay(pointerRay);
        }
    }
}