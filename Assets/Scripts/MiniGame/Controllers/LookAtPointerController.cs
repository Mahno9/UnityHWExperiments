using Navigation.Controllers;

using UnityEngine;

namespace MiniGame
{
    public class LookAtPointerController : ControllerBase
    {
        private readonly Camera               _camera;
        private readonly IRotatableInPosition _rotatable;

        public LookAtPointerController(IRotatableInPosition rotatable)
        {
            _rotatable = rotatable;
            _camera = Camera.main;
        }

        protected override void UpdateLogic(float deltaTime)
        {
            if (_camera is null)
                return;

            Ray   ray   = _camera.ScreenPointToRay(Input.mousePosition);
            Plane plane = new(Vector3.up, new Vector3(0f, _rotatable.Position.y, 0f));

            if (plane.Raycast(ray, out float enter) == false)
                return;

            Vector3 hitPoint  = ray.GetPoint(enter);
            Vector3 direction = hitPoint - _rotatable.Position;
            direction.y = 0f;

            _rotatable.SetLookDirection(direction);
        }
    }
}