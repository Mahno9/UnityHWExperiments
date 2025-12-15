using UnityEngine;
using UnityEngine.Assertions;

namespace InteractionExample.PointerInteractions.DragNDrop
{
    public class DragNDrop : MonoBehaviour
    {
        private const int LeftMouseButton = 0;

        private Camera         _camera;
        private DragNDropLogic _logic;

        private void Start()
        {
            _camera = Camera.main;
            Assert.IsNotNull(_camera);

            _logic = new DragNDropLogic();
        }

        private void Update()
        {
            Ray pointerRay = _camera.ScreenPointToRay(Input.mousePosition);

            if (Input.GetMouseButtonDown(LeftMouseButton))
                _logic.HoldItemOnRay(pointerRay);

            if (Input.GetMouseButtonUp(LeftMouseButton))
                _logic.ReleasePointedItem();
        }

        private void FixedUpdate()
        {
            Ray pointerRay = _camera.ScreenPointToRay(Input.mousePosition);

            _logic.SetItemPositionByRay(pointerRay);
        }
    }
}