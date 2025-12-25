using UnityEngine;

namespace Navigation.Movement.Interfaces
{
    public interface IMovePointSubscriber
    {
        void OnNewMovePoint(Vector3 position);
    }
}